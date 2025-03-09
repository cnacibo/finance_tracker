namespace FinanceTrackerApp;

using CsvHelper;
using System.Globalization;

public class CsvDataImporter : DataImporter{
    protected override void ParseData(string data, List<BankAccount> accounts, List<Category> categories, List<Operation> operations)
    {
        using (var reader = new StringReader(data))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Operation>().ToList();
            operations.AddRange(records);

            var bankAccounts = csv.GetRecords<BankAccount>().ToList();
            accounts.AddRange(bankAccounts);

            var categoryRecords = csv.GetRecords<Category>().ToList();
            categories.AddRange(categoryRecords);
        }
    }
}

