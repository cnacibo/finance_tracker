namespace FinanceTrackerApp;

public class CreateOperationCommand : ICommand{
    private readonly OperationFacade _operationFacade;
    private readonly bool _type;
    private readonly Guid _bankAccountId;
    private readonly double _amount;
    private readonly string _description;
    private readonly Guid _categoryId;
    public CreateOperationCommand(OperationFacade operationFacade, bool type, Guid bankAccountId, double amount, string description, Guid categoryId){
        _operationFacade = operationFacade;
        _type = type;
        _bankAccountId = bankAccountId;
        _amount = amount;
        _description = description;
        _categoryId = categoryId;
    }

    public override void Execute() {
        _operationFacade.CreateOperation(_type, _bankAccountId, _amount, _description, _categoryId);
    }

    public override string ToString() => "Создана новая операция.";

}