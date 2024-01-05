using Microsoft.EntityFrameworkCore;
using SwishBackend.Orders.Models;

namespace SwishBackend.Orders.Data
{
    public class OrdersDbContext : DbContext
    {

        public DbSet<ShoppingCartOrder> ShoppingCartOrders { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
