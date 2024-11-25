using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Middleware;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Authentication;
using eCommerce.Infrastructure.Repositories.Cart;
using eCommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace eCommerce.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureSevice(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "Default";
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString(connectionString),
            sql => {
                sql.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
                sql.EnableRetryOnFailure();
            }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

            services.AddScoped<IGeneric<Product>,GenericRepository<Product>>();
            services.AddScoped<IGeneric<Category>,GenericRepository<Category>>();
            services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdapter<>));

            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = config["JWT:Audience"],
                    ValidIssuer = config["JWT:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
            });
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IRoleManagement, RoleManagement>();
            services.AddScoped<ITokenManagement, TokenManagement>();
            services.AddScoped<IPaymentMethod, PaymentMethodRepo>();
            services.AddScoped<IPaymentService,StripePaymentService>();

            Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

            return services;
        }
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandelingMiddleware>();
            return app;
        }
    }
}
