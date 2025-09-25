
using AIYTVideoSummarizer.Application;
using AIYTVideoSummarizer.Persistence;
using AIYTVideoSummarizer.Infrastructure;
using AIYTVideoSummarizer.Api.Middlewares;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
