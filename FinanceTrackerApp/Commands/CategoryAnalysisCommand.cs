namespace FinanceTrackerApp;

public class CategoryAnalysisCommand : ICommand{
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;

    public CategoryAnalysisCommand(CategoryFacade categoryFacade, OperationFacade operationFacade)
    {
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
    }

    public override void Execute() {
        var strategy = new CategoryGroupingStrategy(_categoryFacade);
        var context = new AnalyticsContext(strategy);
        var analysisResult = context.ExecuteStrategy(_operationFacade);

        Console.WriteLine("\n📈 Аналитика по категориям:");
        if(analysisResult == null){
            Console.WriteLine("Список пустой!");
            return;
        }
        foreach (var result in (Dictionary<string, double>)analysisResult)
        {
            Console.WriteLine($"{result.Key}: {result.Value}");
        }
    }

    public override string ToString() => "Проведен анализ операций по категориям.";

}