using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publicator.Core.Domains.User.Queries;
using Publicator.Core.Helpers;

namespace Publicator.Core
{
    public static class CoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAutoMapper(Assembly.GetAssembly(typeof(PublicatorProfile)));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CurrentUserPipe<,>));
            return services;
        }
    }
}
