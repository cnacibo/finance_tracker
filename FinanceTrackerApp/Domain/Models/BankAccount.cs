using System;
namespace FinanceTrackerApp;

public class BankAccount{
    public Guid Id { get; }
    public string Name { get; private set;}

    public double Balance { get; private set;}

    public BankAccount(Guid id, string name, double balance){
        Id = id;
        Name = name;
        Balance = balance;
    }

    public void AddBalance(double amount){
        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
        } else 
        {
            throw new InvalidOperationException("Недостаточно средств на счете.");
        }
    }

    public void ChangeName(string newName){
        Name = newName;
    }
}