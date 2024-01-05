namespace SwishBackend.StoreItems.Models.Pagination
{
    public interface IPagedRepo
    {
        Task<PagedList<Product>> GetProducts(ProductParameters productParameters);
    }
}
