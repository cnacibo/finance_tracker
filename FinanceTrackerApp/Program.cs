
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

using FinanceTrackerApp;
using System.Reflection.Metadata;

var serviceProvider = new ServiceCollection()
            .AddSingleton<IFinanceFactory, FinanceFactory>()
            .AddSingleton<BankAccountFacade>()
            .AddSingleton<CategoryFacade>()
            .AddSingleton<OperationFacade>()
            // .AddSingleton<FinanceFacade>()
            .BuildServiceProvider();

var bankAccountFacade = serviceProvider.GetService<BankAccountFacade>();
var categoryFacade = serviceProvider.GetService<CategoryFacade>();
var operationFacade = serviceProvider.GetService<OperationFacade>();
// var finance = serviceProvider.GetService<FinanceFacade>();

// Создание счета
BankAccount bankAccount = bankAccountFacade.CreateBankAccount("Дополнительный счет", 50);
// ICommand createAccountCommand = new CreateAccountCommand(bankAccountFacade, "Основной счет", 1000);
// ICommand timedCreateAccountCommand = new TimedCommand(createAccountCommand);
// timedCreateAccountCommand.Execute();


// Создание категории
Category category = categoryFacade.CreateCategory(false, "Продукты");
Category category2 = categoryFacade.CreateCategory(true, "Работа");
Category category3 = categoryFacade.CreateCategory(false, "Дорогие покупки");
// ICommand createCategoryCommand = new CreateCategoryCommand(categoryFacade, false, "Продукты");
// ICommand timedCreateCategoryCommand = new TimedCommand(createCategoryCommand);
// timedCreateCategoryCommand.Execute();


if (bankAccount != null && category!= null){
    // ICommand createOperationCommand = new CreateOperationCommand(operationFacade, false, account.Id, 150, "Покупка еды", Guid.NewGuid());
    // ICommand timedCreateOperationCommand = new TimedCommand(createOperationCommand);
    // timedCreateOperationCommand.Execute();
    Operation operation = operationFacade.CreateOperation(true, bankAccount.Id, 10000, "Зарплата", category2.Id);
    Operation operation2 = operationFacade.CreateOperation(false, bankAccount.Id, 150, "Покупка еды", category.Id);
    Operation operation3 = operationFacade.CreateOperation(false, bankAccount.Id, 2000, "Покупка телефона", category3.Id);
    Operation operation4 = operationFacade.CreateOperation(false, bankAccount.Id, 15000, "Покупка телефизора", category3.Id);
} else{
    Console.WriteLine("Account or Category is null");
}

Console.WriteLine("\n=== банковские счета ===");
foreach(var account in bankAccountFacade.GetAccounts()){
    Console.WriteLine($"ID: {account.Id}, Название: {account.Name}, Баланс: {account.Balance:F2}");
}

Console.WriteLine("\n=== категории ===");
foreach (var categoryy in categoryFacade.GetCategories())
{
    Console.WriteLine($"ID: {categoryy.Id}, Тип: {(categoryy.Type ? "Доход" : "Расход")}, Название: {categoryy.Name}");
}


Console.WriteLine("\n=== операции ===");
foreach (var operation in operationFacade.GetOperations())
{
    Console.WriteLine($"ID: {operation.Id}, Тип: {(operation.Type ? "Пополнение" : "Списание")}, " +
                      $"Счёт: {operation.BankAccountId}, Сумма: {operation.Amount:F2}, " +
                      $"Дата: {operation.Date:yyyy-MM-dd HH:mm:ss}, Описание: {operation.Description}, Категория: {operation.CategoryId}");
}

var strategy = new CategoryGroupingStrategy(categoryFacade);
var context = new AnalyticsContext(strategy);
var analysisResult = context.ExecuteStrategy(operationFacade);

Console.WriteLine("Аналитика по категориям:");
foreach (var result in (Dictionary<string, double>)analysisResult)
{
    Console.WriteLine($"{result.Key}: {result.Value}");
}


DateTime start = new DateTime(2025, 03, 01, 00, 00, 00); // 1 марта 2025, начало дня
DateTime end = new DateTime(2025, 03, 31, 23, 59, 59);   // 31 марта 2025, конец дня

var strategy1 = new IncomeExpenseDifferenceStrategy(start, end);
var context1 = new AnalyticsContext(strategy1);
var analysisResult1 = context1.ExecuteStrategy(operationFacade);

Console.WriteLine("\nПодсчет разницы доходов и расходов: " + analysisResult1.ToString());

// YamlExportVisitor yamlExportVisitor= new YamlExportVisitor();
// yamlExportVisitor.Visit("export.yaml", bankAccountFacade, categoryFacade, operationFacade);
// Console.WriteLine("Экспорт выполнен в Yaml.");

// YamlDataImporter yamlImporter = new YamlDataImporter();
// yamlImporter.ImportData("export.yaml", bankAccountFacade, categoryFacade, operationFacade);

// CsvDataImporter csvImporter = new CsvDataImporter();
// csvImporter.ImportData("export_accounts.csv", bankAccountFacade, categoryFacade, operationFacade);
// csvImporter.ImportData("export_categories.csv", bankAccountFacade, categoryFacade, operationFacade);
// csvImporter.ImportData("export_operations.csv", bankAccountFacade, categoryFacade, operationFacade);

// JsonDataImporter jsonImporter = new JsonDataImporter();

// jsonImporter.ImportData("export.json", bankAccountFacade, categoryFacade, operationFacade);

// Console.WriteLine("=== Импортированные банковские счета ===");
// foreach(var account in bankAccountFacade.GetAccounts()){
//     Console.WriteLine($"ID: {account.Id}, Название: {account.Name}, Баланс: {account.Balance:F2}");
// }

// Console.WriteLine("\n=== Импортированные категории ===");
// foreach (var categoryy in categoryFacade.GetCategories())
// {
//     Console.WriteLine($"ID: {categoryy.Id}, Тип: {(categoryy.Type ? "Доход" : "Расход")}, Название: {categoryy.Name}");
// }

// Console.WriteLine("\n=== Импортированные операции ===");
// foreach (var operation in operationFacade.GetOperations())
// {
//     Console.WriteLine($"ID: {operation.Id}, Тип: {(operation.Type ? "Пополнение" : "Списание")}, " +
//                       $"Счёт: {operation.BankAccountId}, Сумма: {operation.Amount:F2}, " +
//                       $"Дата: {operation.Date:yyyy-MM-dd HH:mm:ss}, Описание: {operation.Description}, Категория: {operation.CategoryId}");
// }

// var exportVisitor = new JsonExportVisitor(); 
// exportVisitor.Visit(bankAccountFacade, categoryFacade, operationFacade);
// Console.WriteLine("Экспорт выполнен в JSON.");

// var exportVisitor2 = new CsvExportVisitor();
// exportVisitor2.Visit("export.csv", bankAccountFacade, categoryFacade, operationFacade);
// Console.WriteLine("\nЭкспорт выполнен в CSV.");

// csvImporter.ImportData("export_accounts.csv", bankAccountFacade, categoryFacade, operationFacade);
// csvImporter.ImportData("export_categories.csv", bankAccountFacade, categoryFacade, operationFacade);
// csvImporter.ImportData("export_operations.csv", bankAccountFacade, categoryFacade, operationFacade);

 