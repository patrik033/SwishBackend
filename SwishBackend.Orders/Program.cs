using MassTransit;
using MassTransitCommons.Common.Email;
using MassTransitCommons.Common.Order;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using SwishBackend.MassTransitCommons.Common.Email;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.MassTransitCommons.Common.Payment.GetSession;
using SwishBackend.MassTransitCommons.Models;
using SwishBackend.Orders;
using SwishBackend.Orders.Data;
using SwishBackend.Orders.Extensions;
using SwishBackend.Orders.Models;
using SwishBackend.Orders.Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStripeService, StripeService>();
await builder.Services.ConfigureAzure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
    x.AddConsumersFromNamespaceContaining<EmailPaymentSuccessMessage>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("order", false));

    x.AddRequestClient<CreateSwishPaymentRequest>();
    x.AddRequestClient<SessionStatusRequest>();
    x.AddRequestClient<CreateStripePaymentSessionRequest>();
    x.AddRequestClient<UserLookupMessage>();
    x.AddRequestClient<ProductLookupMessage>();
    x.AddRequestClient<ShoppingCartOrder>();

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
