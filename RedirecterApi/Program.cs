
using RedirecterCore;
using RedirecterCore.StorageProviders;
using Serilog;

namespace Redirecter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(path: "/var/log/redirect.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14)
                .CreateLogger();

            try
            {

                var builder = WebApplication.CreateBuilder(args);

                logger.Information("Start of Redirect App");

                builder.Host.UseSerilog(logger);
                builder.Services.AddLogging(x => x.AddSerilog(logger));


                builder.Services.AddSingleton(x =>
                {
                    return new RedirectServiceFactory()
                    .SetRedirectServiceProvider(Environment.GetEnvironmentVariable("STORAGE_PROVIDER") switch
                    {
                        "json" => new RedirectStorageProviderJson("redirect.json"),
                        "db" => throw new NotImplementedException(),
                        _ => new RedirectStorageProviderExample()
                    })
                    .CreateService();
                });


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