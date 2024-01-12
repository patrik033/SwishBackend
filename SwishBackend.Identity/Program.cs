using MassTransit;
using MassTransitCommons.Common.Email;
using MassTransitCommons.Common.Order;
using SwishBackend.Identity;
using SwishBackend.Identity.Consumers;
using SwishBackend.Identity.Email.Register;
using SwishBackend.Identity.Email.Token;
using SwishBackend.Identity.Extensions;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Password;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//mailprovider for registering users
builder.Services.AddSingleton<ISendGridEmailRegister, SendGridEmailRegister>();
//mailprovider for tokens
builder.Services.AddSingleton<ISendGridEmailTokens, SendGridEmailTokens>();
//injections for auth services
builder.Services.ConfigureAuthInjections();

//configurations for reset password and confirm email
builder.Services.AddTransient<CustomEmailConfirmationTokenProvider<ApplicationUser>>();
builder.Services.AddTransient<PasswordResetTokenProvider<ApplicationUser>>();




//ServiceExtensions
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureIdentityOptions();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();







builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<EmailConfirmationMessage>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("email", false));


    x.AddConsumer<UserConsumer>()
   .Endpoint(e => e.Name = "userName");
    x.AddRequestClient<UserLookupMessage>(new Uri("exchange:users"));



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

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
SeedData.EnsureDataIsSeeded(app);
app.Run();
