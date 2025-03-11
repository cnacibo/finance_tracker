namespace FinanceTrackerApp;
public class ViewAllAccountsCommand : ICommand
{
    private readonly BankAccountFacade _bankAccountFacade;

    public ViewAllAccountsCommand(BankAccountFacade bankAccountFacade)
    {
        _bankAccountFacade = bankAccountFacade;
    }

    public override void Execute()
    {
        var accounts = _bankAccountFacade.GetAccounts();
        if (accounts.Count == 0)
        {
            Console.WriteLine("Нет доступных счетов.");
            return;
        }

        Console.WriteLine("\n📌 Список всех счетов:");
        foreach (var account in accounts)
        {
            Console.WriteLine($"- ID: {account.Id}\tНазвание: {account.Name}\tБаланс: {account.Balance}");
        }
    }

    public override string ToString() => "Вывод всех счетов.";
}
