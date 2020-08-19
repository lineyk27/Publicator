using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Publicator.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var conn = "DefaultConnection";
            services.AddDbContext<PublicatorDbContext>(options =>
            {
                var Configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                options.UseSqlServer(Configuration.GetConnectionString(conn));
            }, ServiceLifetime.Transient);
            return services;
        }
    }
}
