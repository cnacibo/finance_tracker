namespace FinanceTrackerApp;

public class AnalyticsContext{
    private IAnalyticsStrategy _strategy;

    public AnalyticsContext(IAnalyticsStrategy strategy) {
        _strategy = strategy;
    }

    public object? ExecuteStrategy(OperationFacade operationFacade){
        List<Operation> operations = operationFacade.GetOperations();
        return _strategy?.Analyze(operations);
    }
}