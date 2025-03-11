using System;
namespace FinanceTrackerApp;

public interface IFinanceFactory{
    BankAccount CreateBankAccount(Guid id, string name, double balance);
    Category CreateCategory(Guid id, bool type, string name);
    Operation CreateOperation(Guid id, bool type, Guid bankAccountId, double amount, DateTime date, string description, Guid categoryId);
}