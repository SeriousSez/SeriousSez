using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SeriousSez.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SeriousSez
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RegisterGlobalExceptionHandlers();

            try
            {
                var host = CreateHostBuilder(args).Build();

                CreateDbIfNotExists(host);

                host.Run();
            }
            catch (Exception ex)
            {
                TryWriteFatalStartupLog("Fatal exception during application startup.", ex);
                throw;
            }
        }

        private static void RegisterGlobalExceptionHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
            {
                var exception = eventArgs.ExceptionObject as Exception;
                TryWriteFatalStartupLog("Unhandled exception.", exception);
            };

            TaskScheduler.UnobservedTaskException += (_, eventArgs) =>
            {
                TryWriteFatalStartupLog("Unobserved task exception.", eventArgs.Exception);
            };
        }

        private static void TryWriteFatalStartupLog(string message, Exception exception)
        {
            try
            {
                var baseDirectory = AppContext.BaseDirectory;
                var logsDirectory = Path.Combine(baseDirectory, "logs");
                Directory.CreateDirectory(logsDirectory);

                var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ssZ");
                var logPath = Path.Combine(logsDirectory, $"startup-fatal-{timestamp}.log");

                var content = $"[{DateTime.UtcNow:O}] {message}{Environment.NewLine}{exception}";
                File.WriteAllText(logPath, content);

                var latestLogPath = Path.Combine(logsDirectory, "startup-fatal-latest.log");
                File.WriteAllText(latestLogPath, content);
            }
            catch
            {
            }
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var env = services.GetRequiredService<IHostEnvironment>();
                    if (!env.IsDevelopment())
                    {
                        return;
                    }

                    var context = services.GetRequiredService<SeriousContext>();
                    context.Database.EnsureCreated();
                    // DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.Development.local.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
