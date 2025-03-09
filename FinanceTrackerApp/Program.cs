
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

using FinanceTrackerApp;

var serviceProvider = new ServiceCollection()
            .AddSingleton<IFinanceFactory, FinanceFactory>()
            .AddSingleton<BankAccountFacade>()
            .AddSingleton<CategoryFacade>()
            .AddSingleton<OperationFacade>()
            .AddSingleton<FinanceFacade>()
            .BuildServiceProvider();

var bankAccountFacade = serviceProvider.GetService<BankAccountFacade>();
var categoryFacade = serviceProvider.GetService<CategoryFacade>();
var operationFacade = serviceProvider.GetService<OperationFacade>();
var finance = serviceProvider.GetService<FinanceFacade>();

// Создание счета
ICommand createAccountCommand = new CreateAccountCommand(bankAccountFacade, "Основной счет", 1000);
ICommand timedCreateAccountCommand = new TimedCommand(createAccountCommand);
timedCreateAccountCommand.Execute();


// Создание категории
ICommand createCategoryCommand = new CreateCategoryCommand(categoryFacade, "Продукты", "расход");
ICommand timedCreateCategoryCommand = new TimedCommand(createCategoryCommand);
timedCreateCategoryCommand.Execute();

// Создание операции
var account = bankAccountFacade.GetAccount(Guid.NewGuid());
var createdCategory = categoryFacade.Category(Guid.NewGuid());

if (account != null){
    ICommand createOperationCommand = new CreateOperationCommand(operationFacade, false, account.Id, 150, "Покупка еды", Guid.NewGuid());
    ICommand timedCreateOperationCommand = new TimedCommand(createOperationCommand);
    timedCreateOperationCommand.Execute();
} else{
    Console.WriteLine("Account is null");
}


Console.WriteLine("Accounts count: " + bankAccountFacade.GetAccounts().Count);
Console.WriteLine("Categories count: " + categoryFacade.GetCategories().Count);
Console.WriteLine("Operations count: " + operationFacade.GetOperations().Count);

var exportVisitor = new JsonExportVisitor();
exportVisitor.Visit(bankAccountFacade.GetAccounts(), categoryFacade.GetCategories(), operationFacade.GetOperations());
Console.WriteLine("Экспорт выполнен в JSON.");

var exportVisitor2 = new CsvExportVisitor();
exportVisitor2.Visit(bankAccountFacade.GetAccounts(), categoryFacade.GetCategories(), operationFacade.GetOperations());
Console.WriteLine("Экспорт выполнен в CSV.");

 