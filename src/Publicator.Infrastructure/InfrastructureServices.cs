using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publicator.Infrastructure.Models;
using System;

namespace Publicator.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<PublicatorDbContext>(options =>
            {
                var Configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);

            services
                .AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<PublicatorDbContext>();

            
            return services;
        }
    }
}
