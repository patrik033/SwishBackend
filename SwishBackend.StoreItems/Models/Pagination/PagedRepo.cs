using Microsoft.EntityFrameworkCore;
using SwishBackend.StoreItems.Data;

namespace SwishBackend.StoreItems.Models.Pagination
{
    public class PagedRepo : IPagedRepo
    {
        private readonly StoreItemsDbContext _context;

        public PagedRepo(StoreItemsDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<Product>> GetProducts(ProductParameters parameters)
        {
            var product = await _context.Products.ToListAsync();

            return PagedList<Product>
                .ToPagedList(product, parameters.PageNumber, parameters.PageSize);
        }
    }
}
