using System.Reflection;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Publicator.ApplicationCore
{
    public static class ApplicationCoreServices
    {
        public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(PublicatorProfile)));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAggregationService, AggregationService>();
            return services;
        }
    }
}
