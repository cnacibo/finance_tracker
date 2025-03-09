namespace FinanceTrackerApp;

public class IncomeExpenseDifferenceStrategy : IAnalyticsStrategy{
    private DateTime _start;
    private DateTime _end;
    public IncomeExpenseDifferenceStrategy(DateTime start, DateTime end){
        _start = start;
        _end = end;
    }

    public object Analyze(List<Operation> operations){
        return operations.Where(o => o.Date >= _start && o.Date <= _end).Sum(o => o.Type ? o.Amount : -o.Amount);
    }
}