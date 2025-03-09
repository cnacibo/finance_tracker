namespace FinanceTrackerApp;

public class CategoryFacade{
    private readonly List<Category> _categories = new List<Category>();
    private readonly IFinanceFactory _financeFactory;
    public CategoryFacade(IFinanceFactory financeFactory){
        _financeFactory = financeFactory;
    }

    public Category CreateCategory(bool type, string name){
        Category category = _financeFactory.CreateCategory(type, name);
        _categories.Add(category);
        return category;
    }

    public Category GetCategory(Guid categoryId)
    {
        return _categories.FirstOrDefault(c => c.Id == categoryId);
    }
    
    public List<Category> GetCategories()
    {
        return _categories;
    }

    public void ChangeCategory(Guid categoryId, string name)
    {
        Category category = GetCategory(categoryId);
        category.ChangeName(name);
    }
}