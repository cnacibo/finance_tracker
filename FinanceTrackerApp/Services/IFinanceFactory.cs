namespace FinanceTrackerApp;

public interface IFinanceFactory{
    BankAccount CreateBankAccount(string name, double balance);
    Category CreateCategory(string type, string name);
    Operation CreateOperation(string type, Guid bankAccountId, double amount, DateTime date, string description, Guid categoryId);
}