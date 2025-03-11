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
            bool firstRow = true;
            while (csv.Read())
            {
                if (firstRow) 
                {
                    firstRow = false; // Пропускаем первую строку, которая содержит заголовки
                    continue;
                }
                // Определяем тип данных
                if (csv.Parser.Count == 7)
                {
                    try{
                        operationFacade.AddOperation(csv.GetField<Guid>(0), csv.GetField<string>(1)?.ToLower() == "true",
                        csv.GetField<Guid>(2), csv.GetField<double>(3), csv.GetField<DateTime>(4), 
                        csv.GetField<string>(5) ?? "", csv.GetField<Guid>(6));
                    } catch (Exception e){
                        Console.WriteLine("Не удалось добавить операцию: " + e.Message);
                    }
                    
                }
                else if (csv.Parser.Count == 3)
                {
                    if (Double.TryParse(csv.GetField(2), out _))
                    {
                        try{
                            var name = csv.GetField<string>(1);
                            if (string.IsNullOrEmpty(name))
                            {
                                throw new ArgumentException("Имя аккаунта не может быть null или пустым.");
                            }
                            accountFacade.AddBankAccount(csv.GetField<Guid>(0), name, csv.GetField<double>(2));
                        } catch (Exception e){
                            Console.WriteLine("Не удалось добавить аккаунт: " + e.Message);
                        }    
                    } else { 
                        try{
                            var name = csv.GetField<string>(2);
                            if (string.IsNullOrEmpty(name))
                            {
                                throw new ArgumentException("Название категории не может быть null или пустым.");
                            }
                            categoryFacade.AddCategory(csv.GetField<Guid>(0), csv.GetField<string>(1)?.ToLower() == "true", name);
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

