using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SwishBackend.MassTransitCommons.Common;
using SwishBackend.SignalR.Hubs;

namespace SwishBackend.SignalR.Models
{
    public class ProductUpdatedConsumer : IConsumer<ProductUpdated>
    {
        private readonly IHubContext<ProductHub> _hubContext;

        public ProductUpdatedConsumer(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<ProductUpdated> context)
        {
            var data = context.Message;

            await _hubContext.Clients.All.SendAsync("ProductUpdated", new ProductUpdated
            {
                Id = data.Id,
                Name = data.Name,
                Price = data.Price,
                Image = data.Image,
                Stock = data.Stock,
                ProductCategoryId = data.ProductCategoryId
            });
        }
    }
}
