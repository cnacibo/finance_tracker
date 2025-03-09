namespace FinanceTrackerApp;

public interface IExportVisitor{
    void Visit(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade);
}