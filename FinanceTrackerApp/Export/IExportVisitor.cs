using System;
namespace FinanceTrackerApp;

public interface IExportVisitor{
    void Visit(string fileName, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade);
}