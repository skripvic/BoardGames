using BuisinessLogic.Auth;
using BuisinessLogic.Auth.AuthService;
using BuisinessLogic.Auth.CurrentUser;
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

            services.AddScoped<ICurrentUser, CurrentUser>();
                        
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}
