
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
            .BuildServiceProvider();

var bankAccountFacade = serviceProvider.GetRequiredService<BankAccountFacade>();
var categoryFacade = serviceProvider.GetRequiredService<CategoryFacade>();
var operationFacade = serviceProvider.GetRequiredService<OperationFacade>();

// Создание счета
BankAccount bankAccount = bankAccountFacade.CreateBankAccount("Дополнительный счет", 50);

// Создание категории
Category category = categoryFacade.CreateCategory(false, "Продукты");
Category category2 = categoryFacade.CreateCategory(true, "Работа");
Category category3 = categoryFacade.CreateCategory(false, "Дорогие покупки");

// Создание операций
Operation? operation = operationFacade.CreateOperation(true, bankAccount.Id, 10000, "Зарплата", category2.Id);
Operation? operation2 = operationFacade.CreateOperation(false, bankAccount.Id, 150, "Покупка еды", category.Id);
Operation? operation3 = operationFacade.CreateOperation(false, bankAccount.Id, 2000, "Покупка телефона", category3.Id);
Operation? operation4 = operationFacade.CreateOperation(false, bankAccount.Id, 15000, "Покупка телефизора", category3.Id);

// Вывод общей информации 
ICommand viewAllDataCommand = new ViewAllDataCommand(bankAccountFacade, categoryFacade, operationFacade);
ICommand timedCreateCategoryCommand = new TimedCommand(viewAllDataCommand);
timedCreateCategoryCommand.Execute(); // Замер времени выполнения команды

// Аналитика 
ICommand categoryAnalysisCommand = new CategoryAnalysisCommand(categoryFacade, operationFacade);
categoryAnalysisCommand.Execute();

DateTime start = new DateTime(2025, 03, 01, 00, 00, 00); // 1 марта 2025 00:00:00
DateTime end = new DateTime(2025, 03, 31, 23, 59, 59); // 31 марта 2025 23:59:59

ICommand incomeExpenseAnalysisCommand = new IncomeExpenseAnalysisCommand(operationFacade, start, end);
incomeExpenseAnalysisCommand.Execute();

// Импорт из csv
CsvDataImporter csvDataImporter = new CsvDataImporter();
ICommand importDataCommandCsv = new ImportDataCommand("export_accounts.csv", csvDataImporter, bankAccountFacade, categoryFacade, operationFacade);
importDataCommandCsv.Execute();

ICommand importDataCommandCsv1 = new ImportDataCommand("export_categories.csv", csvDataImporter, bankAccountFacade, categoryFacade, operationFacade);
importDataCommandCsv1.Execute();

ICommand importDataCommandCsv2 = new ImportDataCommand("export_operations.csv", csvDataImporter, bankAccountFacade, categoryFacade, operationFacade);
importDataCommandCsv2.Execute();

// Импорт из yaml
YamlDataImporter yamlImporter = new YamlDataImporter();
ICommand importDataCommand = new ImportDataCommand("export.yaml", yamlImporter, bankAccountFacade, categoryFacade, operationFacade);
ICommand timedImportDataCommand = new TimedCommand(importDataCommand);
timedImportDataCommand.Execute();

// Вывод всех операций
ICommand viewAllOperationsCommand = new ViewAllOperationsCommand(operationFacade, bankAccountFacade, categoryFacade);
viewAllOperationsCommand.Execute();

// Экспорт в json
ICommand exportDataCommandJson = new ExportDataCommand("json", bankAccountFacade, categoryFacade, operationFacade);
ICommand timedExportDataCommandJson = new TimedCommand(exportDataCommandJson);
timedExportDataCommandJson.Execute();

// Экспорт в csv
ICommand exportDataCommandCsv = new ExportDataCommand("csv", bankAccountFacade, categoryFacade, operationFacade);
ICommand timedExportDataCommandCsv = new TimedCommand(exportDataCommandCsv);
timedExportDataCommandCsv.Execute();

 