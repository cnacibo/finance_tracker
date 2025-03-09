namespace FinanceTrackerApp;

public interface IExportVisitor{
    void Visit(List<BankAccount> accounts, List<Category> categories, List<Operation> operations);
}