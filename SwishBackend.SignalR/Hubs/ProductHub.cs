using Microsoft.AspNetCore.SignalR;
using SwishBackend.MassTransitCommons.Common;


namespace SwishBackend.SignalR.Hubs
{
    public class ProductHub : Hub
    {
        public async Task ProductCreated(ProductCreated productCreated)
        {
            await Clients.All.SendAsync("ProductCreated", productCreated);
        }

        public async Task ProductUpdated(ProductUpdated productUpdated)
        {
            await Clients.All.SendAsync("ProductUpdated", productUpdated);
        }

        public async Task ProductDeleted(ProductDeleted productDeleted)
        {
            await Clients.All.SendAsync("ProductDeleted", productDeleted);
        }
    }
}
