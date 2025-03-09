namespace FinanceTrackerApp;

using CsvHelper;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;

public class CsvExportVisitor : IExportVisitor{
    public void Visit(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade){
        List<BankAccount> accounts = accountFacade.GetAccounts();
        List<Category> categories = categoryFacade.GetCategories();
        List<Operation> operations = operationFacade.GetOperations();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            ShouldQuote = (field) => true 
        };

        using (var writer = new StreamWriter("export.csv"))
        using (var csv = new CsvWriter(writer, config))
        {
            // Запись операций
            csv.WriteField("Operation Id");
            csv.WriteField("Type");
            csv.WriteField("Bank Account Id");
            csv.WriteField("Amount");
            csv.WriteField("Date");
            csv.WriteField("Description");
            csv.WriteField("Category Id");
            csv.NextRecord();

            foreach (var operation in operations)
            {
                csv.WriteField(operation.Id);
                csv.WriteField(operation.Type ? "Credit" : "Debit"); // Переводим булевое значение в текст
                csv.WriteField(operation.BankAccountId);
                csv.WriteField(operation.Amount.ToString("F2")); // Форматируем сумму
                csv.WriteField(operation.Date.ToString("yyyy-MM-dd HH:mm:ss")); // Форматируем дату
                csv.WriteField(operation.Description);
                csv.WriteField(operation.CategoryId);
                csv.NextRecord();
            }

            // Запись категорий
            csv.WriteField("Category Id");
            csv.WriteField("Type");
            csv.WriteField("Name");
            csv.NextRecord();

            foreach (var category in categories)
            {
                csv.WriteField(category.Id);
                csv.WriteField(category.Type ? "Income" : "Expense");
                csv.WriteField(category.Name);
                csv.NextRecord();
            }

            // Запись банковских счетов
            csv.WriteField("Account Id");
            csv.WriteField("Name");
            csv.WriteField("Balance");
            csv.NextRecord();

            foreach (var account in accounts)
            {
                csv.WriteField(account.Id);
                csv.WriteField(account.Name);
                csv.WriteField(account.Balance.ToString("F2")); // Форматируем баланс
                csv.NextRecord();
            }
        }
    }
    
}