using System;
using System.Text;
namespace FinanceTrackerApp;

public abstract class DataImporter{

    protected abstract void ParseData(string data, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade);
    public void ImportData(string filePath, BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade){
        try{
            string rawData = File.ReadAllText(filePath, Encoding.UTF8);
            ParseData(rawData, accountFacade, categoryFacade, operationFacade);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Файл {filePath} успешно прочитан!");
            Console.ResetColor();
        } catch(Exception ex){
            Console.WriteLine("Ошибка чтения файла: "+ ex.Message);
        }
        
    }
}