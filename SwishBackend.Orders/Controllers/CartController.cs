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
    public class CartController : ControllerBase
    {

        private readonly IRequestClient<UserLookupMessage> _userClient;
        private readonly IRequestClient<ProductLookupMessage> _productClient;
        private readonly OrdersDbContext _ordersDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public CartController(IPublishEndpoint publishEndpoint, IRequestClient<UserLookupMessage> userClient, IRequestClient<ProductLookupMessage> productClient, OrdersDbContext ordersDbContext)
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
                .GetResponse<UserLookupResponse>(new UserLookupMessage { UserName = userName });

            var product = await _productClient
                .GetResponse<ProductResponseMessage>(new ProductLookupMessage { ProductId = productId });

            var userId = user.Message.UserId;
            var existingOrder = await _ordersDbContext.ShoppingCartOrders
                  .Include(o => o.ShoppingCartItems)
                  .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                  .FirstOrDefaultAsync();

            var item = existingOrder.ShoppingCartItems.FirstOrDefault(x => x.ProductCategoryId == productId);
            if (item != null)
            {
                _ordersDbContext.ShoppingCartItems.Remove(item);
                await _ordersDbContext.SaveChangesAsync();

                existingOrder.TotalPrice = existingOrder.ShoppingCartItems
                       .Sum(item => item.OrderedQuantity * item.Price);

                existingOrder.TotalCount = existingOrder.ShoppingCartItems
                       .Sum(item => item.OrderedQuantity);

                await _ordersDbContext
                    .SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("{userName}/{productId}")]
        public async Task<IActionResult> DecreaseItem(string userName, Guid productId)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>(new UserLookupMessage { UserName = userName });

            var product = await _productClient
                .GetResponse<ProductResponseMessage>(new ProductLookupMessage { ProductId = productId });

            var userId = user.Message.UserId;
            var existingOrder = await _ordersDbContext
                  .ShoppingCartOrders
                  .Include(o => o.ShoppingCartItems)
                  .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                  .FirstOrDefaultAsync();

            var item = existingOrder
                .ShoppingCartItems
                .FirstOrDefault(x => x.ProductCategoryId == productId);


            if (item.OrderedQuantity - 1 > 0)
            {
                item.OrderedQuantity -= 1;
                _ordersDbContext.ShoppingCartItems.Update(item);
                await _ordersDbContext.SaveChangesAsync();

                existingOrder.TotalPrice = existingOrder
                    .ShoppingCartItems
                    .Sum(item => item.OrderedQuantity * item.Price);

                existingOrder.TotalCount = existingOrder
                    .ShoppingCartItems
                    .Sum(item => item.OrderedQuantity);

                await _ordersDbContext
                    .SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/listItems/{userName}")]
        public async Task<IActionResult> GetAllItems(string userName)
        {
            var user = await _userClient.GetResponse<UserLookupResponse>(new UserLookupMessage { UserName = userName });
            if (user?.Message != null)
            {
                var userId = user.Message.UserId;

                var existingOrder = await _ordersDbContext
                    .ShoppingCartOrders
                    .Include(o => o.ShoppingCartItems)
                    .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                    .FirstOrDefaultAsync();

                var existingItem = existingOrder?
                    .ShoppingCartItems
                    .ToList();



                if (existingOrder != null)
                {
                    return Ok(existingOrder);
                }

            }
            return BadRequest();
        }

        [HttpGet("{userName}/{productId}")]
        public async Task<IActionResult> PostOrder(string userName, Guid productId, int quantity)
        {
            var user = await _userClient
                .GetResponse<UserLookupResponse>(new UserLookupMessage { UserName = userName });

            var product = await _productClient
                .GetResponse<ProductResponseMessage>(new ProductLookupMessage { ProductId = productId });

            var counter = 0;

            if (product?.Message != null && user?.Message != null)
            {
                var userId = user.Message.UserId;

                var existingOrder = await _ordersDbContext.ShoppingCartOrders
                    .Include(o => o.ShoppingCartItems)
                    .Where(o => o.UserId == userId && !o.HasBeenCheckedOut)
                    .FirstOrDefaultAsync();

                if (existingOrder != null)
                {
                    var existingItem = existingOrder.ShoppingCartItems
                        .FirstOrDefault(item => item.ProductCategoryId == productId);

                    if (existingItem != null)
                    {
                        if (quantity < product.Message.Stock && quantity > 0)
                        {
                            existingItem.OrderedQuantity += quantity;
                        }
                        else
                        {
                            return BadRequest("Ordered quantity cannot exceed stock quantity.");
                        }
                    }
                    else
                    {
                        if (quantity < product.Message.Stock && quantity > 0)
                        {
                            existingOrder.ShoppingCartItems.Add(new ShoppingCartItem
                            {
                                Name = product.Message.Name,
                                CategoryName = product.Message.CategoryName,
                                Price = product.Message.Price,
                                Stock = product.Message.Stock,
                                OrderedQuantity = quantity,
                                ProductCategoryId = productId,
                            });
                        }
                        else
                        {
                            return BadRequest("Ordered quantity cannot exceed stock quantity.");
                        }
                    }

                    existingOrder.TotalPrice = existingOrder.ShoppingCartItems
                        .Sum(item => item.OrderedQuantity * item.Price);


                    existingOrder.TotalCount = existingOrder.ShoppingCartItems
                           .Sum(item => item.OrderedQuantity);

                    counter = existingOrder.TotalCount;
                    // Save changes to the database
                    //await _ordersDbContext.SaveChangesAsync();
                }

                else
                {
                    if (quantity < product.Message.Stock && quantity > 0)
                    {
                        var newOrder = new ShoppingCartOrder
                        {
                            UserId = userId,
                            ShoppingCartItems = new List<ShoppingCartItem>
                        {
                            new ShoppingCartItem
                            {
                                Name = product.Message.Name,
                                CategoryName = product.Message.CategoryName,
                                Price = product.Message.Price,
                                Stock = product.Message.Stock,
                                OrderedQuantity = quantity,
                                ProductCategoryId = productId,
                            }
                        },
                            OrderTime = DateTime.UtcNow,
                            Email = user.Message.UserName

                        };

                        newOrder.TotalPrice = newOrder.ShoppingCartItems
                            .Sum(item => item.OrderedQuantity * item.Price);

                        newOrder.TotalCount = newOrder.ShoppingCartItems
                            .Sum(item => item.OrderedQuantity);

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
    }
}
