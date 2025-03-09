namespace FinanceTrackerApp;

using CsvHelper;
using System.Globalization;
using System.IO;

public class CsvExportVisitor : IExportVisitor{
    public void Visit(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade){
        List<BankAccount> accounts = accountFacade.GetAccounts();
        List<Category> categories = categoryFacade.GetCategories();
        List<Operation> operations = operationFacade.GetOperations();

        using (var writer = new StreamWriter("export.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(operations);
            csv.WriteRecords(categories);
            csv.WriteRecords(accounts);
        }
    }
}