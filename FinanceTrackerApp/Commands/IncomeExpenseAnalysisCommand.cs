namespace FinanceTrackerApp;

public class IncomeExpenseAnalysisCommand : ICommand{
    private readonly OperationFacade _operationFacade;
    private readonly DateTime _start;
    private readonly DateTime _end;
    public IncomeExpenseAnalysisCommand(OperationFacade operationFacade, DateTime start, DateTime end)
    {
        _operationFacade = operationFacade;
        _start = start;
        _end = end;
    }

    public override void Execute() {
        var strategy = new IncomeExpenseDifferenceStrategy(_start, _end);
        var context = new AnalyticsContext(strategy);
        var analysisResult = context.ExecuteStrategy(_operationFacade);
        Console.WriteLine($"\n📝 Подсчет разницы доходов и расходов с {_start:dd.MM.yyyy} по {_end:dd.MM.yyyy}:");
        Console.WriteLine($"Разница: {analysisResult}");
    }

    public override string ToString() => $"Проведен анализ доходов и расходов с {_start:dd.MM.yyyy} по {_end:dd.MM.yyyy}.";

}