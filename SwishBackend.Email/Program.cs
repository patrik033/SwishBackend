using SwishBackend.Email.Extensions;
using MassTransit;
using SwishBackend.Email.Models.MessageBus;
using SwishBackend.Email.Email;
using SwishBackend.MassTransitCommons.Common.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ISendGridEmailRegisterService, SendGridEmailRegisterService>();
builder.Services.AddSingleton<ISendGridEmailPaymentSuccess, SendGridEmailPaymentSuccess>();
//retrieve the keyvault key
await builder.Services.ConfigureAzure(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<EmailRegistrationSuccessfullConsumer>();
    x.AddConsumersFromNamespaceContaining<EmailPaymentSuccessConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("emailRegistrationQueue", e =>
        {
            //ep.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<EmailRegistrationSuccessfullConsumer>(context);
            // ep.Durable = true;
        });

        cfg.ReceiveEndpoint("emailPaymentQueue", e =>
        {
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<EmailPaymentSuccessConsumer>(context);
        });
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
