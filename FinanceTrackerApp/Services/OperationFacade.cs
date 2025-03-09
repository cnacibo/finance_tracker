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

    public Operation CreateOperation(bool type, Guid bankAccountId, double amount, string description, Guid categoryId){
        Operation operation = _financeFactory.CreateOperation(type, bankAccountId, amount,  DateTime.Now, description, categoryId);
        _operations.Add(operation);

        _bankAccountFacade.UpdateAccountBalance(bankAccountId, type, amount);
        return operation;
    }

     public List<Operation> GetOperations()
    {
        return _operations;
    }

}