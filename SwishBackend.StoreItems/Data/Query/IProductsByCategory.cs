using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data.Query
{
    public interface IProductsByCategory
    {
        public IEnumerable<Product> GetProductByCategory(Guid categoryId);
    }
}
