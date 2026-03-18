using RpaWorker.Models;

namespace RpaWorker.Services;

public interface IScrapingService
{
    Task ScrapeAndSaveAsync();
    IEnumerable<Quote> GetAll();
}
