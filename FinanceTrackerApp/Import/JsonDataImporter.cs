namespace FinanceTrackerApp;

using Newtonsoft.Json;

public class JsonDataImporter : DataImporter{
    protected override void ParseData(string data, List<BankAccount> accounts, List<Category> categories, List<Operation> operations)
    {
        var obj = JsonConvert.DeserializeObject<dynamic>(data);
        accounts.AddRange(obj.accounts.ToObject<List<BankAccount>>());
        categories.AddRange(obj.categories.ToObject<List<Category>>());
        operations.AddRange(obj.operations.ToObject<List<Operation>>());
    }
}

