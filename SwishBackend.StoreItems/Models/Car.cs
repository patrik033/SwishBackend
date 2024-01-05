namespace SwishBackend.StoreItems.Models
{
    public class Car : Product
    {
        public int HorsePower { get; set; }
        public string Model { get; set; }

        public Car()
        {
            Type = ProductType.Car;
        }
    }
}
