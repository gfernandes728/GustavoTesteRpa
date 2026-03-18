using RpaWorker.Models;
using System.Text.Json;

namespace RpaWorker.Services;

public class ScrapingService
(
    HttpClient httpClient,
    ILogger<ScrapingService> logger
) : IScrapingService
{
    private readonly List<Quote> _dataStore = [];

    public async Task ScrapeAndSaveAsync()
    {
        var response = await httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/USD");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonDocument.Parse(json);

        var value = data.RootElement
            .GetProperty("rates")
            .GetProperty("BRL")
            .GetDecimal();

        logger.LogInformation($"Capturado Valor {value} - {DateTime.UtcNow}");

        _dataStore.Add(new() { Value = value });

        logger.LogInformation($"Adicionado Valor {value} - {DateTime.UtcNow}");
        logger.LogInformation($"Total capturado: {_dataStore.Count}");
    }

    public IEnumerable<Quote> GetAll() => _dataStore;
}