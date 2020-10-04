using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using Publicator.Infrastructure;
using Publicator.Presentation.Helpers;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Publicator.Presentation.Handlers;
using Publicator.Core;

namespace Publicator.Presentation
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services
                .AddControllers(options => options.EnableEndpointRouting = false)
                .AddFluentValidation()
                .AddJsonOptions(configuration =>
                {
                    configuration.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddInfrastructureServices();
            services.AddCoreServices();

            services.AddResponseCaching();

            services.AddLogging();
            services.AddSwaggerGen();

            var jwtsettings = _configuration.GetSection("JWTSettings").Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(jwtsettings.SecretKey);
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtsettings.Audience,
                        ValidateIssuer = true,
                        ValidateLifetime = false,
                        ValidIssuer = jwtsettings.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../WebClient/build";
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseResponseCaching();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publicator API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../WebClient";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
