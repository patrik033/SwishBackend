using MassTransit;
using SwishBackend.Payments.Stripe;
using SwishBackend.Payments.Consumer;
using SwishBackend.Payments.Extensions;
using SwishBackend.MassTransitCommons.Common.Payment.CreateSession;
using SwishBackend.MassTransitCommons.Common.Payment.GetSession;
using NgrokAspNetCore;
using SwishBackend.MassTransitCommons.Common.Payment.Swish;
//using SwishBackend.MassTransitCommons.Common.Payment.;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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


builder.Services.AddMassTransit(x =>
{
    
    //x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("payment", false));


    x.AddConsumer<StripeSessionCreateConsumer>()
   .Endpoint( e => e.Name = "paymentOrder");

    x.AddConsumer<SwishCreateConsumer>()
    .Endpoint(e => e.Name = "paymentSwish");

    x.AddConsumer<StripeSessionGetConsumer>()
    .Endpoint(e => e.Name = "paymentGetOrder");

    x.AddRequestClient<CreateStripePaymentSessionResponse>(new Uri("exchange:payments"));
    x.AddRequestClient<SessionStatusResponse>(new Uri("exchange:payments"));
    x.AddRequestClient<CreateSwishPaymentResponse>(new Uri("exchange:payments"));

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

app.Run();
