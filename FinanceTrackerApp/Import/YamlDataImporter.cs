using System;
namespace FinanceTrackerApp;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Collections.Generic;

public class YamlDataImporter : DataImporter
{
    protected override void ParseData(string data, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
    {

        var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

        var obj = deserializer.Deserialize<dynamic>(data);
        var accounts = obj["accounts"];
        var categories = obj["categories"];
        var operations = obj["operations"];
        
        foreach (var acc in accounts)
        {
            try{
                accountFacade.AddBankAccount(
                    Guid.Parse(acc["id"].ToString()),  // Преобразуем строки в Guid
                    acc["name"].ToString(),
                    double.Parse(acc["balance"].ToString())  // Преобразуем строку в double
                );
            } catch (Exception e){
                Console.WriteLine("Не удалось добавить аккаунт: " + e.Message);
            }
        }

        // Импорт категорий
        foreach (var cat in categories)
        {
            try{
                categoryFacade.AddCategory(
                    Guid.Parse(cat["id"].ToString()),  // Преобразуем строки в Guid
                    bool.Parse(cat["type"].ToString()),  // Преобразуем строку в bool
                    cat["name"].ToString()
                );
            } catch (Exception e) {
                Console.WriteLine("Не удалось добавить категорию: " + e.Message);
            }
        }

        // Импорт операций
        foreach (var op in operations)
        {
            try{
                operationFacade.AddOperation(
                    Guid.Parse(op["id"].ToString()),  // Преобразуем строки в Guid
                    bool.Parse(op["type"].ToString()),  // Преобразуем строку в bool
                    Guid.Parse(op["bankAccountId"].ToString()),  // Преобразуем строки в Guid
                    double.Parse(op["amount"].ToString()),  // Преобразуем строку в double
                    DateTime.Parse(op["date"].ToString()),  // Преобразуем строку в DateTime
                    op["description"].ToString(),
                    Guid.Parse(op["categoryId"].ToString())  // Преобразуем строки в Guid
                );
            } catch (Exception e){
                Console.WriteLine("Не удалось добавить операцию: " + e.Message);
            }
        }
    }
}
