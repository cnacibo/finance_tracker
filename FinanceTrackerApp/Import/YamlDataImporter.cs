namespace FinanceTrackerApp;

using YamlDotNet.Serialization;

public class YamlDataImporter : DataImporter
{
    protected override void ParseData(string data, List<BankAccount> accounts, List<Category> categories, List<Operation> operations)
    {
        var deserializer = new DeserializerBuilder().Build();
        var obj = deserializer.Deserialize<dynamic>(data);
        
        accounts.AddRange(obj.accounts.ToObject<List<BankAccount>>());
        categories.AddRange(obj.categories.ToObject<List<Category>>());
        operations.AddRange(obj.operations.ToObject<List<Operation>>());
    }
}
