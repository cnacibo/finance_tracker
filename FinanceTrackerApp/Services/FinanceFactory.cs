namespace FinanceTrackerApp;

public class FinanceFactory : IFinanceFactory{
    public BankAccount CreateBankAccount(string name, double balance){
        return new BankAccount(name,balance);
    }
    public Category CreateCategory(bool type, string name){
        return new Category(type,name);
    }
    public Operation CreateOperation(bool type, Guid bankAccountId, double amount, 
    DateTime date, string description, Guid categoryId){
        return new Operation(type, bankAccountId, amount, date, description, categoryId);
    }
}