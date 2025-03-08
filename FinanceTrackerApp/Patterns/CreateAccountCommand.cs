namespace FinanceTrackerApp;

public class CreateAccountCommand : ICommand{
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly string _name;
    private readonly double _balance;

    public CreateAccountCommand(BankAccountFacade bankAccountFacade, string name, double balance){
        _bankAccountFacade = bankAccountFacade;
        _name = name;
        _balance = balance;
    }

    public override void Execute() {
        _bankAccountFacade.CreateBankAccount(_name, _balance);
    }

    public override string ToString() => "Создан новый аккаунт.";

}