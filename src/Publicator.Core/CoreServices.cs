using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Publicator.Core.Domains.User.Queries;

namespace Publicator.Core
{
    public static class CoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CurrentUserPipe<,>));
            services.AddValidatorsFromAssemblyContaining(typeof(IPageRequest));
            return services;
        }
    }
}
