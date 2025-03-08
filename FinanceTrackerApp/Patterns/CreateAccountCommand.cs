namespace FinanceTrackerApp;

public class CreateAccountCommand : ICommand{
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly string _name;
    private readonly double _balance;

    public CreateAccountCommand(BankAccountFacade bankAccountFacade, string name, double b