using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Publicator.Core;
using Publicator.Infrastructure;
using Publicator.Presentation.Handlers;
using Publicator.Presentation.Helpers;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

namespace Publicator.Presentation
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddSerilog();

            services.AddHttpContextAccessor();
            services.AddControllers(options => options.EnableEndpointRouting = false)
                .AddJsonOptions(configuration =>
                {
                    configuration.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddSwaggerGen();

            var jwtsettings = _configuration.GetSection("JWTSettings").Get<JWTSettings>();
            var key = Encoding.UTF8.GetBytes(jwtsettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
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

            services.AddExceptionHandler<ExceptionHandler>();

            services.AddInfrastructureServices();
            services.AddCoreServices();
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
            app.UseExceptionHandler(_ => { });

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publicator API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(builder =>
            {
                builder.Options.SourcePath = "../ClientApp";

                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
                }
            });
        }
    }
}
