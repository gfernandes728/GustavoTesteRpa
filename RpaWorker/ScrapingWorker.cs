using RpaWorker.Services;

namespace RpaWorker;

public class ScrapingWorker
(
    IScrapingService scrapingService,
    ILogger<ScrapingWorker> logger
) : BackgroundService
{
    private readonly int MinutesTimeSpanError = 1;
    private readonly int MinutesTimeSpanSuccess = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        LoggerInformation($"Iniciando Service - {DateTime.UtcNow}");

        while (!stoppingToken.IsCancellationRequested)
        {
            var minutesTimeSpan = MinutesTimeSpanSuccess;

            try
            {
                logger.LogInformation($"Iniciando Captura - {DateTime.UtcNow}");

                var result = await scrapingService.ScrapeAndSaveAsync();
                if (!result) minutesTimeSpan = MinutesTimeSpanError;
            }
            catch (Exception ex)
            {
                LoggerInformation($"Erro no RPA: {ex.Message}", true);
                minutesTimeSpan = MinutesTimeSpanError;
            }

            await Task.Delay(TimeSpan.FromMinutes(minutesTimeSpan), stoppingToken);
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