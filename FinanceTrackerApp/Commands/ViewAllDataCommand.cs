namespace FinanceTrackerApp;
using System;

public class ViewAllDataCommand : ICommand
{
    private readonly ICommand _viewAccounts;
    private readonly ICommand _viewCategories;
    private readonly ICommand _viewOperations;

    public ViewAllDataCommand(BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
    {
        _viewAccounts = new ViewAllAccountsCommand(bankAccountFacade);
        _viewCategories = new ViewAllCategoriesCommand(categoryFacade);
        _viewOperations = new ViewAllOperationsCommand(operationFacade, bankAccountFacade, categoryFacade);
    }

    public override void Execute()
    {
        Console.WriteLine("\n📊 🔸 ВЫВОД ВСЕЙ ФИНАНСОВОЙ ИНФОРМАЦИИ 🔸 📊");
        _viewAccounts.Execute();
        _viewCategories.Execute();
        _viewOperations.Execute();
    }

    public override string ToString() => "Вывод всех счетов, категорий и операций.";
}