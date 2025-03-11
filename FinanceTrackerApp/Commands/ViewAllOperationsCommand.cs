using System.Runtime.InteropServices.Marshalling;

namespace FinanceTrackerApp;

public class ViewAllOperationsCommand : ICommand
{
    private readonly OperationFacade _operationFacade;
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;
    

    public ViewAllOperationsCommand(OperationFacade operationFacade, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade)
    {
        _operationFacade = operationFacade;
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
    }

    public override void Execute()
    {
        var operations = _operationFacade.GetOperations();
        if (operations.Count == 0)
        {
            Console.WriteLine("Нет доступных операций.");
            return;
        }

        Console.WriteLine("\n📌 Список всех операций:");
        foreach (var operation in operations)
        {
            string type = operation.Type ? "Доход" : "Расход";
            BankAccount? account = _bankAccountFacade.GetAccount(operation.BankAccountId);
            Category? category = _categoryFacade.GetCategory(operation.CategoryId);
            string name;
            string category_name;
            if (category == null){
                category_name = " ";
            }  else{
                category_name = category.Name;
            }
            if(account == null){
                name = " ";
            } else{
                name = account.Name;
            }
            Console.WriteLine($"- ID: {operation.Id}\tТип: {type}\tСчет: {name}\tСумма: {operation.Amount}\tДата: {operation.Date}\tОписание: {operation.Description}\tКатегория: {category_name}");
        }
    }

    public override string ToString() => "Вывод всех операций.";
}
