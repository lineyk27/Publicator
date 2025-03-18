using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Publicator.Core;
using Publicator.Core.Helpers;
using Publicator.Infrastructure;
using Publicator.Presentation.Handlers;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

namespace Publicator.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            builder.Services.AddSerilog();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers()
                .AddJsonOptions(configuration =>
                {
                    configuration.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddSwaggerGen();

            var jwtsettings = builder.Configuration.GetSection("JWTSettings").Get<JWTSettings>();
            var key = Encoding.UTF8.GetBytes(jwtsettings.SecretKey);
            builder.Services.AddAuthentication(options =>
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

            builder.Services.AddExceptionHandler<ExceptionHandler>();

            builder.Services.AddInfrastructureServices();
            builder.Services.AddCoreServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Publicator API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseExceptionHandler(_ => { });

            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../ClientApp";

                if (app.Environment.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
                }
            });

            app.Run();
        }
    }
}
