using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SwishBackend.MassTransitCommons.Common;
using SwishBackend.SignalR.Hubs;

namespace SwishBackend.SignalR.Models
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {

        private readonly IHubContext<ProductHub> _hubContext;

        public ProductCreatedConsumer(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var data = context.Message;

            await _hubContext.Clients.All.SendAsync("ProductCreated", new ProductCreated
            {
                Id = data.Id,
                Name = data.Name,
                Price = data.Price,
                Image = data.Image,
                Stock = data.Stock
            });
        }
    }
}
