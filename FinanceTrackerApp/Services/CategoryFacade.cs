namespace FinanceTrackerApp;

public class CategoryFacade{
    private readonly List<Category> _categories = new List<Category>();
    private readonly IFinanceFactory _financeFactory;
    public CategoryFacade(IFinanceFactory financeFactory){
        _financeFactory = financeFactory;
    }

    public void CreateCategory(string type, string name){
        Category category = _financeFactory.CreateCategory(type, name);
        _categories.Add(category);
    }

    public Category Category(Guid categoryId)
    {
        return _categories.FirstOrDefault(c => c.Id == categoryId);
    }

    
    public List<Category> GetCategories()
        {
            return _categories;
        }


}