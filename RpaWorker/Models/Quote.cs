namespace RpaWorker.Models;

public class Quote
{
    public string Currency { get; set; } = "BRL";
    public decimal Value { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
