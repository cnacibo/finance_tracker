namespace FinanceTrackerApp;

public class BankAccountFacade{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();
    private readonly IFinanceFactory _financeFactory;
    public BankAccountFacade(IFinanceFactory financeFactory){
        _financeFactory = financeFactory;
    }

    public void CreateBankAccount(string name, double balance){
        BankAccount bankAccount = _financeFactory.CreateBankAccount(name, balance);
        _accounts.Add(bankAccount);
    }

    public BankAccount GetAccount(Guid accountId)
    {
        return _accounts.FirstOrDefault(a => a.Id == accountId);
    }

    public void UpdateAccountBalance(Guid accountId, double amount){
        BankAccount bankAccount = GetAccount(accountId);
        bankAccount.UpdateBalance(amount);
    }
    public List<BankAccount> GetAccounts(){
            return _accounts;
        }

    
}