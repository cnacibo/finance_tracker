namespace FinanceTrackerApp;
using System;

public class Operation{
    public Guid Id { get;}
    public bool Type { get; private set; } // true - add, false - substract 
    public Guid BankAccountId {get;}
    public double Amount{ get; }
    public DateTime Date {get;}
    public string? Description{get; private set;}
    public Guid CategoryId {get;}

    public Operation(Guid id, bool type, Guid bankAccountId, double amount, DateTime date, string description, Guid categoryId){
        if (amount <= 0) {
            throw new ArgumentException("Сумма операции должна быть положительной.");
        }
        Id = id;
        Type = type;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = date;
        Description = description;
        CategoryId = categoryId;
    }

    public void ChangeDescription(string newDescription){
        Description = newDescription;
    }
}