using System;
namespace FinanceTrackerApp;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;

public class JsonExportVisitor : IExportVisitor{
    public void Visit(string fileName, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade){
        List<BankAccount> accounts = accountFacade.GetAccounts();
        List<Category> categories = categoryFacade.GetCategories();
        List<Operation> operations = operationFacade.GetOperations();

        File.WriteAllText(fileName, JsonConvert.SerializeObject(new { accounts, categories, operations }, Formatting.Indented));
    }
}