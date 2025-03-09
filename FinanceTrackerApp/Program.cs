
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


var exportVisitor = new JsonExportVisitor();
exportVisitor.Visit(bankAccountFacade, categoryFacade, operationFacade);
Console.WriteLine("Экспорт выполнен в JSON.");

var exportVisitor2 = new CsvExportVisitor();
exportVisitor2.Visit(bankAccountFacade, categoryFacade, operationFacade);
Console.WriteLine("Экспорт выполнен в CSV.");

 