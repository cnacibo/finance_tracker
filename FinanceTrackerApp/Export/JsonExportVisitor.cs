using System;
namespace FinanceTrackerApp;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;

public class JsonExportVisitor : IExportVisitor{
    public void Visit(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade){
        List<BankAccount> accounts = accountFacade.GetAccounts();
        List<Category> categories = categoryFacade.GetCategories();
        List<Operation> operations = operationFacade.GetOperations();

        File.WriteAllText("export.json", JsonConvert.SerializeObject(new { accounts, categories, operations }, Formatting.Indented));
    }
}