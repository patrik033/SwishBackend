using MassTransit;
using MassTransitCommons.Common.Order;
using Microsoft.EntityFrameworkCore;
using SwishBackend.Orders;
using SwishBackend.Orders.Data;
using SwishBackend.Orders.Extensions;
using SwishBackend.Orders.Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStripeService, StripeService>();
await builder.Services.ConfigureAzure(builder.Configuration);

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    });


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<OrderNotify>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("order", false));




    x.AddRequestClient<UserLookupMessage>();
    x.AddRequestClient<ProductLookupMessage>();

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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
SeedData.EnsureDataIsSeeded(app);
app.Run();
