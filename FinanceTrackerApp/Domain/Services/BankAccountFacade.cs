using System;
namespace FinanceTrackerApp;

public class BankAccountFacade{
    private readonly List<BankAccount> _accounts = new List<BankAccount>();
    private readonly IFinanceFactory _financeFactory;
    public BankAccountFacade(IFinanceFactory financeFactory){
        _financeFactory = financeFactory;
    }

    public BankAccount CreateBankAccount(string name, double balance){
        Guid guid_id = Guid.NewGuid();
        BankAccount bankAccount = _financeFactory.CreateBankAccount(guid_id, name, balance);
        _accounts.Add(bankAccount);
        return bankAccount;
    }

    public void AddBankAccount(Guid guid_id, string name, double balance){
        if (GetAccount(guid_id) == null)
        {
            BankAccount bankAccount = _financeFactory.CreateBankAccount(guid_id, name, balance);
            _accounts.Add(bankAccount);
        } else {
            throw new ArgumentException("Аккаунт с таким id уже существует");
        }
    
    }

    public BankAccount? GetAccount(Guid accountId)
    {
        return _accounts.FirstOrDefault(a => a.Id == accountId);
    }

    
    public List<BankAccount> GetAccounts()
    {
        return _accounts;
    }

    public void UpdateAccountBalance(Guid bankAccountId, bool type, double amount)
    {
        BankAccount? bankAccount = GetAccount(bankAccountId);
        if (bankAccount == null){
            return;
        }
        if (type){
            bankAccount.AddBalance(amount);
         } else{
            bankAccount.Withdraw(amount);
        }
    }

    public void ChangeBankAccount(Guid accountId, string name)
    {
        BankAccount? bankAccount = GetAccount(accountId);
        if (bankAccount != null){
            bankAccount.ChangeName(name);
            Console.WriteLine($"\nАккаунт с id {accountId} изменен.");
        } else {
            Console.WriteLine($"\nАккаунт с id {accountId} не найден.");
        }
        
    }

    public bool DeleteBankAccount(Guid accountId)
    {
        BankAccount? bankAccount = GetAccount(accountId);
        if (bankAccount != null)
        {
            _accounts.Remove(bankAccount); // Удаляем аккаунт из списка
            Console.WriteLine($"\nАккаунт с id {accountId} удален.");
            return true; // Возвращаем true, если удаление прошло успешно
        }
        else
        {
            Console.WriteLine($"\nАккаунт с id {accountId} не найден.");
            return false; // Возвращаем false, если аккаунт не найден
        }
    }  
}