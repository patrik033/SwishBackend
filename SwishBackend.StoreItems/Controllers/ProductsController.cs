using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SwishBackend.MassTransitCommons.Common;
using SwishBackend.StoreItems.Data;
using SwishBackend.StoreItems.Models;
using SwishBackend.StoreItems.Models.Dtos.ProductEntitites;
using SwishBackend.StoreItems.Models.Pagination;


namespace SwishBackend.StoreItems.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly StoreItemsDbContext _context;
        private readonly IBus _bus;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly IPagedRepo _pagedRepo;

        public ProductsController(
            IBus bus,
            IPublishEndpoint publishEndpoint,
            StoreItemsDbContext context,
            IMapper mapper,
            IPagedRepo pagedRepo
            )
        {
            _context = context;
            _mapper = mapper;
            _bus = bus;
            _publishEndpoint = publishEndpoint;
            _pagedRepo = pagedRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProducts = await _context.Products.ToListAsync();
            return Ok(allProducts);
        }

        [HttpGet("/paged")]
        public async Task<IActionResult> GetPaged([FromQuery] ProductParameters productParameters)
        {
            var products = await _pagedRepo.GetProducts(productParameters);

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return Ok(products);
        }

        [HttpGet("/category/{id}")]

        public async Task<IActionResult> GetByCategory(Guid id)
        {
            var allFromCategory = await _context.Products.Where(x => x.ProductCategoryId == id).ToListAsync();
            return Ok(allFromCategory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var getProductById = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (getProductById != null)
            {
                return Ok(getProductById);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (productDto != null)
            {
                var product = _mapper.Map<Product>(productDto);

                // _context.Products.Add(product);

                var newProduct = _mapper.Map<ProductDto>(product);
                await _publishEndpoint.Publish(_mapper.Map<ProductCreated>(newProduct));
                //var result = await _context.SaveChangesAsync() > 0;

                //if (!result) return BadRequest("Could not save changes to DB");

                //return CreatedAtAction(nameof(GetById), new { product.Id }, newProduct);
                return Ok();

            }
            return BadRequest();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdatedDto updatedDto)
        {
            var getProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (getProduct == null) return NotFound();

            getProduct.Name = updatedDto.Name;
            getProduct.Price = updatedDto.Price;
            getProduct.Image = updatedDto.Image;
            getProduct.Stock = updatedDto.Stock;

            if (updatedDto.ProductCategoryId == Guid.Empty)
                getProduct.ProductCategoryId = getProduct.ProductCategoryId;
            else
                getProduct.ProductCategoryId = updatedDto.ProductCategoryId;

            await _publishEndpoint.Publish(_mapper.Map<ProductUpdated>(getProduct));

            //var result = await _context.SaveChangesAsync() > 0;
            //if(result) return Ok();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();

            // _context.Products.Remove(product);

            await _publishEndpoint.Publish<ProductDeleted>(new { Id = product.Id.ToString() });

            //var result = await _context.SaveChangesAsync() > 0;
            //if (!result) return BadRequest("Could not save changes to db");

            return Ok();
        }


    }
}
