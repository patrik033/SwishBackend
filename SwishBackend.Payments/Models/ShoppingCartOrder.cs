using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SwishBackend.Payments.Models
{
    public class ShoppingCartOrder
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
        public bool HasBeenCheckedOut { get; set; } = false;
        public DateTime OrderTime { get; set; }
        [Precision(18,2)]
        public decimal? TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public string Email { get; set; }
    }
}
