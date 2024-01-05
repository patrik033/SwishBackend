using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SwishBackend.MassTransitCommons.Common;
using SwishBackend.SignalR.Hubs;

namespace SwishBackend.SignalR.Models
{
    public class ProductDeletedConsumer : IConsumer<ProductDeleted>
    {
        private readonly IHubContext<ProductHub> _hubContext;
        public ProductDeletedConsumer(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<ProductDeleted> context)
        {
            var data = context.Message;

            await _hubContext.Clients.All.SendAsync("ProductDeleted", new ProductDeleted
            {
                Id = data.Id
            });
        }
    }
}
