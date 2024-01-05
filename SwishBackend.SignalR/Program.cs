using MassTransit;
using MassTransitCommons.Common.Order;
using SwishBackend.SignalR.Hubs;
using SwishBackend.SignalR.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<ProductCreatedConsumer>();
    x.AddConsumersFromNamespaceContaining<ProductUpdatedConsumer>();
    x.AddConsumersFromNamespaceContaining<OrderNotify>();
    //x.AddConsumer<ProductConsumer>();
    // x.AddHealthChecks();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {

            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order", e =>
        {
            //ep.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<OrderConsumer>(context);
            // ep.Durable = true;
        });


        cfg.ReceiveEndpoint("productCreatedQueue", e =>
        {
            //ep.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<ProductCreatedConsumer>(context);
            // ep.Durable = true;
        });

        cfg.ReceiveEndpoint("productUpdatedQueue", e =>
        {
            //ep.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<ProductUpdatedConsumer>(context);
            // ep.Durable = true;
        });

        cfg.ReceiveEndpoint("productDeletedQueue", e =>
        {
            //ep.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<ProductDeletedConsumer>(context);
            // ep.Durable = true;
        });
    });



});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddHubOptions<ProductHub>(options =>
{
    options.EnableDetailedErrors = true;
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseWebSockets();
app.MapHub<ProductHub>("/productHub").RequireCors("ReactCorsPolicy");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
