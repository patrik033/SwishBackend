using System.Runtime.Serialization;

namespace SwishBackend.StoreItems.Models
{
    public enum ProductType
    {
        [EnumMember(Value = "Car")]
        Painting,
        [EnumMember(Value = "Painting")]
        Car
    }
}
