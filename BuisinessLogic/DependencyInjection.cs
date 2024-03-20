using BuisinessLogic.Auth;
using BuisinessLogic.Settings;

namespace BuisinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, AuthSettings? authSettings)
        {
            if (authSettings is null)
            {
                throw new ArgumentNullException(nameof(authSettings), "Не заданы настройки аутентификации");
            }

            services.AddSingleton(authSettings);

            services.AddScoped<IAuthService, AuthService>();
                        
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}
