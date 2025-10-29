
using AIYTVideoSummarizer.Application;
using AIYTVideoSummarizer.Persistence;
using AIYTVideoSummarizer.Infrastructure;
using AIYTVideoSummarizer.Api.Middlewares;
using AIYTVideoSummarizer.Application.Common;
using AIYTVideoSummarizer.Api.Common.Extensions;
using AIYTVideoSummarizer.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace AIYTVideoSummarizer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.Configure<AppSettings>(
                builder.Configuration.GetSection(AppSettings.AppSettingsSectionName));
            builder.Services.Configure<GoogleSettings>(
                builder.Configuration.GetSection("Authentication:Google"));

            var jwtOptions = builder.Configuration.GetSection("Authentication").Get<JwtOptions>();
            builder.Services.AddAuthenticationServices(jwtOptions!)
                .AddAuthorizationPolicies();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
