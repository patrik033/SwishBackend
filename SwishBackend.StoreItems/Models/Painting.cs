namespace SwishBackend.StoreItems.Models
{
    public class Painting : Product
    {
        public string PainterName { get; set; }
        public Painting()
        {
            Type = ProductType.Painting;
        }
    }
}
