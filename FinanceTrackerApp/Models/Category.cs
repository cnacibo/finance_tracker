namespace FinanceTrackerApp;

public class Category{
    public Guid Id { get; } = Guid.NewGuid();
    public string? Type { get; set; }
    public string? Name { get; set; }

    publi