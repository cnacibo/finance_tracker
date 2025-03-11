using System;
namespace FinanceTrackerApp;

using Newtonsoft.Json;

public class JsonDataImporter : DataImporter{
    protected override void ParseData(string data, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
    {
        var obj = JsonConvert.DeserializeObject<dynamic>(data);
        if (obj == null){
            throw new Exception("Проблемы с конвертацией.");
        }
        var accounts = obj.accounts.ToObject<List<BankAccount>>();
        var categories = obj.categories.ToObject<List<Category>>();
        var operations = obj.operations.ToObject<List<Operation>>();

        foreach (var account in accounts)
        {
            try{
                accountFacade.AddBankAccount(account.Id, account.Name, account.Balance);
            } catch (Exception e){
                Console.WriteLine("Не удалось добавить аккаунт: " + e.Message);
            }    
        }

        // Добавляем категории через фасад
        foreach (var category in categories)
        {
            try{
                categoryFacade.AddCategory(category.Id, category.Type, category.Name);
            } catch (Exception e) {
                Console.WriteLine("Не удалось добавить категорию: " + e.Message);
            }
            
        }

        // Добавляем операции через фасад
        foreach (var operation in operations)
        {
            try{
                operationFacade.AddOperation(operation.Id, operation.Type, operation.BankAccountId, operation.Amount, operation.Date, operation.Description, operation.CategoryId);
            } catch (Exception e){
                Console.WriteLine("Не удалось добавить операцию: " + e.Message);
            }
            
        }
    }
}

