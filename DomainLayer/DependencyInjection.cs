namespace DomainLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection Add(this IServiceCollection services)
        {
            Console.WriteLine("hello");
            return services;
        }
    }
}
