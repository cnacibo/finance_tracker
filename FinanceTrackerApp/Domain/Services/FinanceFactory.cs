using System;
namespace FinanceTrackerApp;

public class FinanceFactory : IFinanceFactory{
    public BankAccount CreateBankAccount(Guid id, string name, double balance){
        return new BankAccount(id, name, balance);
    }
    public Category CreateCategory(Guid id, bool type, string name){
        return new Category(id, type, name);
    }
    public Operation CreateOperation(Guid id,  bool type, Guid bankAccountId, double amount, 
    DateTime date, string description, Guid categoryId){
        return new Operation(id, type, bankAccountId, amount, date, description, categoryId);
    }
}