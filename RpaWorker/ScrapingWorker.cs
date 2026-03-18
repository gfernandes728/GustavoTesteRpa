using RpaWorker.Services;

namespace RpaWorker;

public class ScrapingWorker
(
    IScrapingService scrapingService,
    ILogger<ScrapingWorker> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        LoggerInformation($"Iniciando Service - {DateTime.UtcNow}");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation($"Iniciando Captura - {DateTime.UtcNow}");

                await scrapingService.ScrapeAndSaveAsync();
            }
            catch (Exception ex)
            {
                LoggerInformation($"Erro no RPA: {ex.Message}", true);
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }

        LoggerInformation($"Finalizando Service - {DateTime.UtcNow}");
    }

    private void LoggerInformation
    (
        string message,
        bool isError = false
    )
    {
        if (isError)
        {
            logger.LogError("============================================================");
            logger.LogError(message);
            logger.LogError("============================================================");
            return;
        }

        logger.LogInformation("============================================================");
        logger.LogInformation(message);
        logger.LogInformation("============================================================");
    }
}