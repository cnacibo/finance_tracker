using System;
namespace FinanceTrackerApp;

using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

public class CsvDataImporter : DataImporter{
    protected override void ParseData(string data, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
    {
        using (var reader = new StringReader(data))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            MissingFieldFound = null,
            HeaderValidated = null
        })){
            Console.WriteLine("Читаем CSV...");
            bool firstRow = true;
            while (csv.Read())
            {

                if (firstRow) 
                {
                    firstRow = false; // Пропускаем первую строку, которая содержит заголовки
                    continue;
                }
                
                // Определяем, какая это секция 
                if (csv.Parser.Count == 7)
                {
                    Console.WriteLine("\nЧитаем операцию...");
                    try{
                        operationFacade.AddOperation(csv.GetField<Guid>(0), csv.GetField<string>(1).ToLower() == "true",
                        csv.GetField<Guid>(2), csv.GetField<double>(3), csv.GetField<DateTime>(4), 
                        csv.GetField<string>(5), csv.GetField<Guid>(6));
                        Console.WriteLine("Операция успешно добавлена");
                    } catch (Exception e){
                        Console.WriteLine("Не удалось добавить операцию: " + e.Message);
                    }
                    
                }
                else if (csv.Parser.Count == 3)
                {
                    if (Double.TryParse(csv.GetField(2), out _))
                    {
                        Console.WriteLine("\nЧитаем аккаунт...");
                        try{
                            accountFacade.AddBankAccount(csv.GetField<Guid>(0), csv.GetField<string>(1), csv.GetField<double>(2));
                            Console.WriteLine("Аккаунт успешно добавлен");
                        } catch (Exception e){
                            Console.WriteLine("Не удалось добавить аккаунт: " + e.Message);
                        }    
                    } else { 
                        Console.WriteLine("\nЧитаем категорию...");
                        try{
                            categoryFacade.AddCategory(csv.GetField<Guid>(0), csv.GetField<string>(1).ToLower() == "true", csv.GetField<string>(2));
                            Console.WriteLine("Категория успешно добавлена");
                        } catch (Exception e) {
                            Console.WriteLine("Не удалось добавить категорию: " + e.Message);
                        }
                    }
                }
                else {
                    continue;
                }
            }
        }
        
    }
}

