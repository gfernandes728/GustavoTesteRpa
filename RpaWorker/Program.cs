using RpaWorker;
using RpaWorker.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IScrapingService, ScrapingService>();
        services.AddHostedService<ScrapingWorker>();
    })
    .Build();

await host.RunAsync();