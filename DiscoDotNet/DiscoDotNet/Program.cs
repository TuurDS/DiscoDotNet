using DiscoDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using DiscoDotNet.Services;
using DiscoDotNet.Services.Intefaces;

var builder = Host.CreateDefaultBuilder(args)
                  .UseContentRoot(AppDomain.CurrentDomain.BaseDirectory);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Logging
var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext();

Log.Logger = loggerConfiguration.CreateLogger();

builder.ConfigureLogging((context, loggingBuilder) =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(Log.Logger);
});

builder.ConfigureServices((hostContext, services) =>
{
    //Appsettings
    services.AddOptions<AppSettings>()
        .Bind(configuration);

    //mediator
    services.AddMediator(mediator =>
    {
        mediator.AddConsumers(typeof(Program).Assembly);
    });

    services.AddHostedService<StartDisco>();
    services.AddSingleton<IDiscordConnectClient, DiscordConnectClient>();

});

var app = builder.Build();

app.Run();
