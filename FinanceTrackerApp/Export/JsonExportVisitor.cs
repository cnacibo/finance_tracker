namespace FinanceTrackerApp;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;

public class JsonExportVisitor : IExportVisitor{
    public void Visit(List<BankAccount> accounts, List<Category> categories, List<Operation> operations){
        File.WriteAllText("export.json", JsonConvert.SerializeObject(new { accounts, categories, operations }, Formatting.Indented));
    }
}