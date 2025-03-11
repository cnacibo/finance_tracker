using System;
namespace FinanceTrackerApp;

public class OperationFacade{
    private readonly List<Operation> _operations = new List<Operation>();
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly IFinanceFactory _financeFactory;
    
    public OperationFacade(IFinanceFactory financeFactory, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade){
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _financeFactory = financeFactory;
    }

    public Operation? CreateOperation(bool type, Guid bankAccountId, double amount, string description, Guid categoryId){
        Operation operation;
        try{
            Guid guid_id = Guid.NewGuid();
            BankAccount? account = _bankAccountFacade.GetAccount(bankAccountId);
            if (account == null){
                throw new ArgumentException("Аккаунта с таким id не существует");
            }
            Category? category = _categoryFacade.GetCategory(categoryId);
            if (category == null){
                throw new ArgumentException("Категории с таким id не существует");
            }
            operation = _financeFactory.CreateOperation(guid_id, type, bankAccountId, amount,  DateTime.Now, description, categoryId);
            _bankAccountFacade.UpdateAccountBalance(bankAccountId, type, amount);
            _operations.Add(operation);
            Console.WriteLine("-- Операция: " + (type? "Пополнение" : "Снятие") + ", Сумма = " + amount + ", Текущий баланс = " + account.Balance);
            return operation;
        } catch (Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Операция отклонена: " + e.Message);
            Console.ResetColor();
            return null;
        }
    }

    public void AddOperation(Guid id, bool type, Guid bankAccountId, double amount, DateTime date, string description, Guid categoryId){
        if (GetOperation(id) == null){
            Operation operation= _financeFactory.CreateOperation(id, type, bankAccountId, amount, date, description, categoryId);
             _operations.Add(operation);
        } else {
            throw new ArgumentException("Операция с таким id уже существует");
        }
    
    }

     public List<Operation> GetOperations()
    {
        return _operations;
    }

    public Operation? GetOperation(Guid Id)
    {
        return _operations.FirstOrDefault(a => a.Id == Id);
    }

    public void ChangeBankAccount(Guid operationId, string description)
    {
        Operation? operation = GetOperation(operationId);

        if (operation != null){
            operation.ChangeDescription(description);
            Console.WriteLine($"\nОперация с id {operationId} изменена.");
        } else {
            Console.WriteLine($"\nОперация с id {operationId} не найдена.");
        }
        
    }

    public bool DeleteOperation(Guid operationId){
        Operation? operation = GetOperation(operationId);

        if (operation != null)
        {
            _operations.Remove(operation); // Удаляем  из списка
            Console.WriteLine($"Операция с id {operationId} удалена.");
            return true; // Возвращаем true, если удаление прошло успешно
        }
        else
        {
            Console.WriteLine($"Операция с id {operationId} не найдена.");
            return false; // Возвращаем false, если не найдена
        }
    }



}