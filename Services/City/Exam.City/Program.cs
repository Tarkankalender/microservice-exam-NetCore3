using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.City
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               //.Enrich.WithCorrelationId()
               .Enrich.FromLogContext()
               .Enrich.WithProperty("Application", "Exam.City")
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
               .MinimumLevel.Override("System", LogEventLevel.Warning)
               .WriteTo.Elasticsearch(
                   new ElasticsearchSinkOptions(
                       new Uri("http://localhost:9200/"))
                   {
                       CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                       AutoRegisterTemplate = true,
                       TemplateName = "serilog-events-template",
                       IndexFormat = "Exam.City-log-{0:yyyy.MM.dd}"
                   })
               .MinimumLevel.Warning()
               .CreateLogger();
            Log.Information("Exam.City WebApi Starting...");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Error(e, "@e");
            }
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //  Host.CreateDefaultBuilder(args)
        //      .ConfigureWebHostDefaults(webBuilder =>
        //      {
        //          webBuilder.UseStartup<Startup>();
        //      });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(config => config.ClearProviders()).UseSerilog();
    }
}
