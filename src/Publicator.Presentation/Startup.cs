using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Publicator.Infrastructure;
using Publicator.ApplicationCore;
using Publicator.ApplicationCore.Helpers;
using Publicator.Presentation.Helpers;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Publicator.Presentation.Handlers;
using Publicator.Core;

namespace Publicator.Presentation
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services
                .AddControllers(options => options.EnableEndpointRouting = false)
                //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                //.AddNewtonsoftJson(options =>
                //    {
                //        //options.SerializerSettings.Converters.Add(new StringEnumConverter());
                //        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //    })
                .AddJsonOptions(configuration =>
                {
                    configuration.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    //configuration.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddInfrastructureServices();
            services.AddApplicationCoreServices();
            services.AddCoreServices();

            services.AddLogging();
            services.AddSwaggerGen(c =>
            {
                //c.DescribeAllEnumsAsStrings();
            });

            services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));
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
                configuration.RootPath = "ClientApp/build";
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
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
