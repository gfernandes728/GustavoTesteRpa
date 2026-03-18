using RpaWorker.Models;

namespace RpaWorker.Services;

public interface IScrapingService
{
    Task<bool> ScrapeAndSaveAsync();
    IEnumerable<Quote> GetAll();
}
