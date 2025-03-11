namespace FinanceTrackerApp;

public class CategoryGroupingStrategy : IAnalyticsStrategy{
    private List<Category> _categories;
    public CategoryGroupingStrategy(CategoryFacade categoryFacade){
        _categories = categoryFacade.GetCategories();
    }

    public object Analyze(List<Operation> operations){
        return operations.GroupBy(o => o.CategoryId).ToDictionary(g => _categories.First(c => c.Id == g.Key).Name,
         g => g.Sum(o => o.Type ? o.Amount : -o.Amount));
    }
}