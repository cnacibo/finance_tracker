namespace FinanceTrackerApp;

public class Operation{
    public Guid Id { get; } = Guid.NewGuid();
    public string? Type { get; }
    public Guid BankAccountId {get;}
    public double Amount{ get; }
    public DateTime Date {get;}
    public string? Description{get;}
    public Guid CategoryId {get;}

    public Operation(string type, Guid bankAccountId, double amount, DateTime date, string description, Guid categoryId){
        if (amount <= 0) {
            throw new ArgumentException("Сумма операции должна быть положительной.");
        }
        Type = type;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = date;
        Description = description;
        CategoryId = categoryId;
    }
}