using Microsoft.EntityFrameworkCore;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            services.AddDbContext<SystemDbContext>(opt => { opt.UseSqlServer(configuration.GetConnectionString("MyConnection"));});

            return services;
        }
    }
}
