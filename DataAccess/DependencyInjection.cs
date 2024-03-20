using DataAccess.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, DatabaseSettings? databaseSettings)
        {
            if (databaseSettings == null)
            {
                throw new NullReferenceException("Не задано подключение к базе данных");
            }

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(databaseSettings.DefaultConnection));

            services.AddScoped<IApplicationDbContext>(isp => isp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services, SymmetricSecurityKey signingKey)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Is-Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
