namespace FinanceTrackerApp;

public class AnalyticsContext{
    private IAnalyticsStrategy _strategy;

    public AnalyticsContext(IAnalyticsStrategy strategy) {
        _strategy = strategy;
    }

    public object ExecuteStrategy(List<Operation> operations){
        return _strategy?.Analyze(operations);
    }
}