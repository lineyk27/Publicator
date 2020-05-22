using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var conn = "DefaultConnection";
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<PublicatorDbContext>(options =>
            {
                var Configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                options.UseSqlServer(Configuration.GetConnectionString(conn));
            }, ServiceLifetime.Transient);
            return services;
        }
    }
}
