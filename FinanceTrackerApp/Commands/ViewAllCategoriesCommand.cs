namespace FinanceTrackerApp;
public class ViewAllCategoriesCommand : ICommand
{
    private readonly CategoryFacade _categoryFacade;

    public ViewAllCategoriesCommand(CategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    public override void Execute()
    {
        var categories = _categoryFacade.GetCategories();
        if (categories.Count == 0)
        {
            Console.WriteLine("Нет доступных категорий.");
            return;
        }

        Console.WriteLine("\n📌 Список всех категорий:");
        foreach (var category in categories)
        {
            string type = category.Type ? "Доход" : "Расход";
            Console.WriteLine($"- ID: {category.Id}\tНазвание: {category.Name}\tТип: {type}");
        }
    }

    public override string ToString() => "Вывод всех категорий.";
}
