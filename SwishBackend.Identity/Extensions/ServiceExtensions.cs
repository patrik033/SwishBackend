using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SwishBackend.Identity.Authentication;
using SwishBackend.Identity.Data;
using SwishBackend.Identity.Email.Token;
using SwishBackend.Identity.Models;
using SwishBackend.Identity.Password;
using System.Text;

namespace SwishBackend.Identity.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
        //identity
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
            }).AddEntityFrameworkStores<ApplicationDbContext>();
        }
        //identity options
        public static void ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;

                options.Tokens.ProviderMap.Add("CustomEmailConfirmation",
                    new TokenProviderDescriptor(
                        typeof(CustomEmailConfirmationTokenProvider<ApplicationUser>)));

                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";

                options.Tokens.ProviderMap.Add("PasswordReset",
                    new TokenProviderDescriptor(
                        typeof(PasswordResetTokenProvider<ApplicationUser>)));

                options.Tokens.PasswordResetTokenProvider = "PasswordReset";

                //options.Password.RequireDigit = false;
                //options.Password.RequiredLength = 1;
                //options.Password.RequireLowercase = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireNonAlphanumeric = false;
            });
        }
        //authenticatíon options
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetValue<string>("ApiSettings:Secret");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        //swagger config
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
             {
                 options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                     "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                     "Example: \"Bearer 12345aasdäd\"",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Scheme = JwtBearerDefaults.AuthenticationScheme
                 });
                 options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
             {
             new OpenApiSecurityScheme
                {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 },
                 Scheme = "oauth2",
                 Name = "Bearer",
                 In = ParameterLocation.Header
                },
                 new List<string>()
             }
                });
             });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureAuthInjections(this IServiceCollection services)
        {
           services.AddScoped<IUserRegistrationService, UserRegistrationService>();
           services.AddScoped<IUserLoginService, UserLoginService>();
           services.AddScoped<IConfirmAccountService, ConfirmAccountService>();
           services.AddScoped<IUpdateAccount, UpdateAccount>();
           services.AddScoped<IResetService, ResetService>();
        }




        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
