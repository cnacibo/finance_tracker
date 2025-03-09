namespace FinanceTrackerApp;

using CsvHelper;
using System.Globalization;
using System.IO;

public class CsvExportVisitor : IExportVisitor{
    public void Visit(List<BankAccount> accounts, List<Category> categories, List<Operation> operations){
        using (var writer = new StreamWriter("export.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(operations);
            csv.WriteRecords(categories);
            csv.WriteRecords(accounts);
        }
    }
}