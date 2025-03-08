namespace FinanceTrackerApp;

public class Category{
    public Guid Id { get; } = Guid.NewGuid();
    public string? Type { get; set; }
    public string? Name { get; set; }

    public Category(string type, string name){
        Type = type;
        Name = name;
    }

}