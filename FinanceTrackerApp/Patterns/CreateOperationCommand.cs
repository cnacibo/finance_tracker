namespace FinanceTrackerApp;

public class CreateOperationCommand : ICommand{
    private readonly OperationFacade _operationFacade;
    private readonly string _type;
    private readonly Guid _bankAccountId;
    private readonly double _amount;
    private readonly string _description;
    private readonly Guid _categoryId;
    public CreateOperationCommand(OperationFacade operationFacade, string type, Guid bankAccountId, double amount, string description, Guid categoryId){
        _operationFacade = operationFacade;
        _type = type;
        _bankAccountId = bankAccountId;
        _amount = a