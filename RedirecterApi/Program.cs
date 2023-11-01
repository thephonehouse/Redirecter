
using Serilog;

namespace Redirecter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new Serilog.LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
                .WriteTo.Console()
#else
                .MinimumLevel.Information()
#endif
                .WriteTo.File(path: "/var/log/redirect.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14)
                .CreateLogger();

            try
            {

                var builder = WebApplication.CreateBuilder(args);

                logger.Information("Start of Redirect App");

                builder.Host.UseSerilog(logger);
                builder.Services.AddLogging(x => x.AddSerilog(logger));

                builder.Services.AddSingleton(x =>
                 new RedirectServiceFactory("/test").CreateExampleService());

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something went wrong.");
            }
        }
    }
}