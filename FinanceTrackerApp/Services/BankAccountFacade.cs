namespace FinanceTrackerApp;

public class BankAccountFacade{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();
    private readonly IFinanceFactory _financeFactory;
    public BankAccountFacade(IFinanceFactory financeFactory){
        _financeFactory = financeFactory;
    }

    public BankAccount CreateBankAccount(string name, double balance){
        BankAccount bankAccount = _financeFactory.CreateBankAccount(name, balance);
        _accounts.Add(bankAccount);
        return bankAccount;
    }

    public BankAccount GetAccount(Guid accountId)
    {
        return _accounts.FirstOrDefault(a => a.Id == accountId);
    }

    
    public List<BankAccount> GetAccounts()
    {
            return _accounts;
    }

    public void UpdateAccountBalance(Guid bankAccountId, bool type, double amount)
    {
        try{
            BankAccount bankAccount = GetAccount(bankAccountId);
            if (type){
                bankAccount.AddBalance(amount);
            } else{
                bankAccount.Withdraw(amount);
            }
            Console.WriteLine("operation is successful");
        } catch (Exception e) {
            Console.WriteLine("operation failed: " + e.Message);
        }
    }
    
}