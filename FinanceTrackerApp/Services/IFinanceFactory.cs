namespace FinanceTrackerApp;

public interface IFinanceFactory{
    BankAccount CreateBankAccount(string name, double balance);
    Category CreateCategory(str