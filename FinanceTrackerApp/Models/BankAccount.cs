namespace FinanceTrackerApp;

public class BankAccount{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set;}

    public double Balance { get; private set;}

    public BankAccount(string name, double balance){
        Name = name;
        Balance = balance;
    }

    public void UpdateBalance(double amount){
        Balance += amount;
    }
}