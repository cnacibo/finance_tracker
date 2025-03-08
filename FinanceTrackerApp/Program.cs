
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
            .BuildServiceProvider();

var bankAccountFacade = serviceProvider.GetService<BankAccountFacade>();
var categoryFacade = serviceProvider.GetService<CategoryFacade>();
var operationFacade = serviceProvider.GetService<OperationFacade>();

// Создание счета
ICommand createAccountCommand = new CreateAccountCommand(bankAccountFacade, "Основной счет", 1000);
ICommand timedCreateAccountCommand = new TimedCommand(createAccountCommand);
timedCreateAccountCommand.Execute();

// Создание категории
ICommand createCategoryCommand = new CreateCategoryCommand(categoryFacade, "Продукты", "расход");
ICommand timedCreateCategoryCommand = new TimedCommand(createCategoryCommand);
timedCreateCategoryCommand.Execute();

// Создание операции
var account = bankAccountFacade.GetAccount(Guid.NewGuid()); // Пример, реальный Id счета должен быть использован
if (account != null){
    ICommand createOperationCommand = new CreateOperationCommand(operationFacade, "расход", account.Id, 150, "Покупка еды", Guid.NewGuid());
    ICommand timedCreateOperationCommand = new TimedCommand(createOperationCommand);
    timedCreateOperationCommand.Execute();
} else{
    Console.WriteLine("Account is null");
}
