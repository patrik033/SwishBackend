using MassTransit;
using MassTransitCommons.Common.Order;
using SwishBackend.MassTransitCommons.Common;
using SwishBackend.StoreItems.Consumers;
using SwishBackend.StoreItems.Data.Query;
using SwishBackend.StoreItems.Extensions;
using SwishBackend.StoreItems.Models.Pagination;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<ProductCreated>();
    x.AddConsumersFromNamespaceContaining<ProductUpdated>();
    x.AddConsumersFromNamespaceContaining<ProductDeleted>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("product", false));

    x.AddConsumer<ProductConsumer>()
   .Endpoint(e => e.Name = "productId");
    x.AddRequestClient<ProductLookupMessage>(new Uri("exchange:orderProducts"));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});


builder.Services.AddScoped<IPagedRepo, PagedRepo>();
builder.Services.AddScoped<IProductsByCategory, ProductsByCategory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
