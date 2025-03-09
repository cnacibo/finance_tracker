namespace FinanceTrackerApp;

public class FinanceFacade{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();
    private readonly List<Category> _categories = new List<Category>();
    private readonly List<Operation> _operations = new List<Operation>();
    private readonly IFinanceFactory _factory;

    public FinanceFacade(IFinanceFactory factory)
    {
        _factory = factory;
    }

    public void CreateAccount(string name, double balance)
    {
        _accounts.Add(_factory.CreateBankAccount(name, balance));
    }

    public void CreateCategory(string name, string type)
    {
        _categories.Add(_factory.CreateCategory(name, type));
    }

    public void CreateOperation(bool type, Guid bankAccountId, double amount, string description, Guid categoryId)
    {
        var operation = _factory.CreateOperation(type, bankAccountId, amount, DateTime.Now, description, categoryId);
        _operations.Add(operation);
        var account = _accounts.FirstOrDefault(a => a.Id == bankAccountId);
        if (account != null)
        {
            account.UpdateBalance(type == true ? amount : -amount);
        }
    }
    
}