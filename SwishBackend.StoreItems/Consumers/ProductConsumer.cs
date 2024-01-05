using AutoMapper;
using MassTransit;
using MassTransitCommons.Common.Order;
using Microsoft.EntityFrameworkCore;
using SwishBackend.StoreItems.Data;

namespace SwishBackend.StoreItems.Consumers
{
    public class ProductConsumer : IConsumer<ProductLookupMessage>
    {
        private readonly StoreItemsDbContext _storeItemsDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductConsumer> _logger;

        public ProductConsumer(StoreItemsDbContext storeItemsDbContext,IMapper mapper, ILogger<ProductConsumer> logger)
        {
            _storeItemsDbContext = storeItemsDbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ProductLookupMessage> context)
        {
            var product = await _storeItemsDbContext.Products
                .Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == context.Message.ProductId);
            if(product != null)
            {
                try
                {
                var mappedType = _mapper.Map<ProductResponseMessage>(product);
                await context.RespondAsync<ProductResponseMessage>(mappedType);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error responding with ProductResponseMessage");
                }
            }
        }
    }
}
