using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace CommomLibrary.Logging
{
    /// <summary>
    /// 設定Serilog
    /// 在主專案的Program中執行UseSerilogSetting
    /// </summary>
    public static class SerilogSettingExtensions
    {
        /// <summary>
        /// 註冊Serilog
        /// </summary>
        /// <param name="builder"></param>
        public static void UseSerilogSetting(this WebApplicationBuilder builder)
        {
            try
            {
                Log.Information("Starting web host");
                //controller可以使用ILogger介面來寫入log紀錄
                builder.Host.UseSerilog((context, services, configuration) => configuration
                            .ReadFrom.Configuration(context.Configuration) // 從appsettings.json設定檔中讀取
                            .ReadFrom.Services(services)
                            .Enrich.FromLogContext()
                        );
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// 註冊Serilog寫進Elasticsearch中
        /// </summary>
        /// <param name="builder"></param>
        public static void UseSerilogWithElasticsearchSetting(this WebApplicationBuilder builder)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                })
                .Enrich.WithProperty("Environment", environment!)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            builder.Host.UseSerilog();
        }
    }
}
