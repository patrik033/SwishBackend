using MassTransit;
using MassTransitCommons.Common.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwishBackend.Orders.Data;
using SwishBackend.Orders.Models;

namespace SwishBackend.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IRequestClient<UserLookupMessage> _userClient;
        private readonly IRequestClient<ProductLookupMessage> _productClient;
        private readonly OrdersDbContext _ordersDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderController(IPublishEndpoint publishEndpoint, IRequestClient<UserLookupMessage> userClient, IRequestClient<ProductLookupMessage> productClient, OrdersDbContext ordersDbContext)
        {
            _publishEndpoint = publishEndpoint;
            _userClient = userClient;
            _productClient = productClient;
            _ordersDbContext = ordersDbContext;
        }

        [HttpDelete("{userName}/{productId}")]
        public async Task<IActionResult> RemoveItem(string userName, Guid productId)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>
                (new UserLookupMessage { UserName = userName });

            var product = await _productClient
                .GetResponse<ProductResponseMessage>
                (new ProductLookupMessage { ProductId = productId });

            var userId = user.Message.UserId;
            var existingOrder = await _ordersDbContext
                  .ShoppingCartOrders
                  .Include(o => o.ShoppingCartItems)
                  .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                  .FirstOrDefaultAsync();

            var item = existingOrder.ShoppingCartItems
                .FirstOrDefault(x => x.ProductCategoryId == productId);

            if (item != null)
            {
                _ordersDbContext
                    .ShoppingCartItems
                    .Remove(item);

                await _ordersDbContext
                    .SaveChangesAsync();

                //updating orders totalprice and count after update
                CalculateTotalPrice(existingOrder);
                CalculateTotalCount(existingOrder);

                await _ordersDbContext
                    .SaveChangesAsync();


                var orderCount = await _ordersDbContext
                  .ShoppingCartOrders
                  .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                  .FirstOrDefaultAsync();

                var notify = new OrderNotify
                {
                    Count = orderCount.TotalCount,
                    Email = orderCount.Email,
                    UserId = orderCount.UserId,
                };

                await _publishEndpoint.Publish(notify);

                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("{userName}/{productId}")]
        public async Task<IActionResult> DecreaseItem(string userName, Guid productId)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>
                (new UserLookupMessage { UserName = userName });

            var product = await _productClient
                .GetResponse<ProductResponseMessage>
                (new ProductLookupMessage { ProductId = productId });


            var userId = user.Message.UserId;
            var existingOrder = await _ordersDbContext
                  .ShoppingCartOrders
                  .Include(o => o.ShoppingCartItems)
                  .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                  .FirstOrDefaultAsync();

            var item = existingOrder
                .ShoppingCartItems
                .FirstOrDefault(x => x.ProductCategoryId == productId);



            if (item.OrderedQuantity - 1 >= 0)
            {
                item.OrderedQuantity -= 1;
                _ordersDbContext.ShoppingCartItems
                    .Update(item);

                await _ordersDbContext
                    .SaveChangesAsync();
                //updating orders totalprice and count after update
                CalculateTotalPrice(existingOrder);
                CalculateTotalCount(existingOrder);

                await _ordersDbContext
                    .SaveChangesAsync();


                var orderCount = await _ordersDbContext
                  .ShoppingCartOrders
                  .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                  .FirstOrDefaultAsync();

                var notify = new OrderNotify
                {
                    Count = orderCount.TotalCount,
                    Email = orderCount.Email,
                    UserId = orderCount.UserId,
                };

                await _publishEndpoint.Publish(notify);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpGet("/current/{userName}")]
        public async Task<IActionResult> GetCurrentUserCount(string userName)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>
                (new UserLookupMessage { UserName = userName });

            if (user.Message != null)
            {
                var userId = user.Message.UserId;

                var orderCount = await _ordersDbContext
                    .ShoppingCartOrders
                    .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                    .FirstOrDefaultAsync();

                if (orderCount != null)
                {

                    var notify = new OrderNotify
                    {
                        Count = orderCount.TotalCount,
                        Email = orderCount.Email,
                        UserId = orderCount.UserId,
                    };
                    await _publishEndpoint
                        .Publish(notify);

                    return Ok();
                }
                return NotFound();
            }

            return BadRequest();
        }

        [HttpGet("/listItems/{userName}")]
        public async Task<IActionResult> GetAllItems(string userName)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>
                (new UserLookupMessage { UserName = userName });

            if (user?.Message != null)
            {
                var userId = user.Message.UserId;

                var existingOrder = await _ordersDbContext
                    .ShoppingCartOrders
                    .Include(o => o.ShoppingCartItems)
                    .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                    .Select(o => new
                    {
                        TotalAmount = o.TotalPrice,
                        TotalCount = o.TotalCount,
                        ShoppingCartItems = o.ShoppingCartItems
                        .Select(item => new
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Price = item.Price,
                            Quantity = item.OrderedQuantity,
                            Category = item.ProductCategoryId,
                            CategoryName = item.CategoryName,
                            Stock = item.Stock,
                            Image = item.Image,

                        })
                    }).FirstOrDefaultAsync();

                if (existingOrder != null)
                {
                    return Ok(existingOrder);
                }

            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] PostOrderRequestObject requestObject)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>
                (new UserLookupMessage { UserName = requestObject.userName });

            var product = await _productClient
                .GetResponse<ProductResponseMessage>
                (new ProductLookupMessage { ProductId = requestObject.productId });

            var counter = 0;

            if (product?.Message != null && user?.Message != null)
            {
                var userId = user.Message.UserId;
                var existingOrder = await GetExistingOrderAsync(userId);

                if (existingOrder != null)
                {
                    var existingItem = existingOrder
                        .ShoppingCartItems
                        .FirstOrDefault(item => item.ProductCategoryId == requestObject.productId);

                    if (existingItem != null)
                    {
                        if (requestObject.quantity < product.Message.Stock && requestObject.quantity > 0)
                        {
                            existingItem.OrderedQuantity += requestObject.quantity;
                        }
                        else
                        {
                            return BadRequest("Ordered quantity cannot exceed stock quantity.");
                        }
                    }
                    else
                    {
                        if (requestObject.quantity < product.Message.Stock && requestObject.quantity > 0)
                        {
                            AddToExistingOrder(requestObject.productId, requestObject.quantity, product, existingOrder);
                        }
                        else
                        {
                            return BadRequest("Ordered quantity cannot exceed stock quantity.");
                        }
                    }

                    CalculateTotalPrice(existingOrder);
                    CalculateTotalCount(existingOrder);

                    counter = existingOrder.TotalCount;
                    // Save changes to the database
                    //await _ordersDbContext.SaveChangesAsync();
                }

                else
                {
                    if (requestObject.quantity < product.Message.Stock && requestObject.quantity > 0)
                    {
                        ShoppingCartOrder newOrder = CreateNewOrder(requestObject.productId, requestObject.quantity, user, product, userId);
                        CalculateTotalPrice(newOrder);
                        CalculateTotalCount(newOrder);

                        counter = newOrder.TotalCount;

                        _ordersDbContext.ShoppingCartOrders
                            .Add(newOrder);
                    }
                    else
                    {
                        return BadRequest("Ordered quantity cannot exceed stock quantity.");
                    }
                }

                await _ordersDbContext.SaveChangesAsync();


                var cartItem = new ProductCartOrder
                {
                    UserId = user.Message.UserId,
                    Email = user.Message.UserName,
                    Product = product.Message
                };

                var notify = new OrderNotify
                {
                    Count = counter,
                    Email = cartItem.Email,
                    UserId = cartItem.UserId,
                };
                await _publishEndpoint.Publish(notify);
                return Ok(new { cartItem });
            }
            return BadRequest("Either User or order was incorrect");
        }

        private static void AddToExistingOrder(Guid productId, int quantity, Response<ProductResponseMessage> product, ShoppingCartOrder? existingOrder)
        {
            existingOrder.ShoppingCartItems.Add(new ShoppingCartItem
            {
                Name = product.Message.Name,
                CategoryName = product.Message.CategoryName,
                Image = product.Message.Image,
                Price = product.Message.Price,
                Stock = product.Message.Stock,
                OrderedQuantity = quantity,
                ProductCategoryId = productId,
            });
        }

        private static ShoppingCartOrder CreateNewOrder(Guid productId, int quantity, Response<UserLookupResponse> user, Response<ProductResponseMessage> product, string userId)
        {
            return new ShoppingCartOrder
            {
                UserId = userId,
                ShoppingCartItems = new List<ShoppingCartItem>
                        {
                            new ShoppingCartItem
                            {
                                Name = product.Message.Name,
                                CategoryName = product.Message.CategoryName,
                                Image = product.Message.Image,
                                Price = product.Message.Price,
                                Stock = product.Message.Stock,
                                OrderedQuantity = quantity,
                                ProductCategoryId = productId,
                            }
                        },
                OrderTime = DateTime.UtcNow,
                Email = user.Message.UserName
            };
        }

        private async Task<ShoppingCartOrder?> GetExistingOrderAsync(string userId)
        {
            return await _ordersDbContext
                .ShoppingCartOrders
                .Include(o => o.ShoppingCartItems)
                .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                .FirstOrDefaultAsync();
        }

        private static void CalculateTotalCount(ShoppingCartOrder? existingOrder)
        {
            existingOrder.TotalCount = existingOrder
                                .ShoppingCartItems
                                .Sum(item => item.OrderedQuantity);
        }

        private static void CalculateTotalPrice(ShoppingCartOrder? existingOrder)
        {
            existingOrder.TotalPrice = existingOrder
                .ShoppingCartItems
                .Sum(item => item.OrderedQuantity * item.Price);
        }
    }
}
