using MassTransit;
using MassTransitCommons.Common.Order;
using Microsoft.AspNetCore.SignalR;
using SwishBackend.SignalR.Hubs;

namespace SwishBackend.SignalR.Models
{
    public class OrderConsumer : IConsumer<OrderNotify>
    {
        private readonly IHubContext<ProductHub> _hubContext;

        public OrderConsumer(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<OrderNotify> context)
        {
            var data = context.Message;

            await _hubContext.Clients.All.SendAsync("order", new OrderNotify
            {
                Count = data.Count,
                Email = data.Email,
                UserId = data.UserId,
            });
        }
    }
}
