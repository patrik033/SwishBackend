using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data.Query
{
    public class ProductsByCategory : IProductsByCategory
    {
        private readonly StoreItemsDbContext _context;

        public ProductsByCategory(StoreItemsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductByCategory(Guid categoryId)
        {
            return _context.Products.Where(p => p.ProductCategoryId == categoryId).ToList();
        }

    }
}
