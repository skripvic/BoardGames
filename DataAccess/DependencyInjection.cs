using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string? connectionString)
        {

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            //services.AddScoped<IApplicationDbContext>(isp => isp.GetRequiredService<ApplicationDbContext>());

            return services;
        }


    }
}
