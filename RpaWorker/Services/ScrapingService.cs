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
        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"Falha ao capturar valor da Api [https://api.exchangerate-api.com/v4/latest/USD] {DateTime.UtcNow}.");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            logger.LogError($"Sem dados capturados da Api [https://api.exchangerate-api.com/v4/latest/USD] {DateTime.UtcNow}.");
            return;
        }

        var value = GetValue(data: JsonDocument.Parse(json));
        if (!value.HasValue)
        {
            logger.LogError($"Sem valores de USD para BRL, capturados da Api [https://api.exchangerate-api.com/v4/latest/USD] {DateTime.UtcNow}.");
            return;
        }

        logger.LogInformation($"Capturado Valor {value} - {DateTime.UtcNow}");

        _dataStore.Add(new() { Value = value.Value });

        logger.LogInformation($"Adicionado Valor {value} - {DateTime.UtcNow}");
        logger.LogInformation($"Total capturado: {_dataStore.Count}");
    }

    public IEnumerable<Quote> GetAll() => _dataStore;

    private decimal? GetValue(JsonDocument? data)
    {
        if  (
                data is not null &&
                data.RootElement.TryGetProperty("rates", out var rates) &&
                rates.TryGetProperty("BRL", out var valueBrl)
            )
        {
            return valueBrl.GetDecimal();
        }

        return null;
    }
}