using System;
namespace FinanceTrackerApp;

public class CategoryFacade{
    private readonly List<Category> _categories = new List<Category>();
    private readonly IFinanceFactory _financeFactory;
    public CategoryFacade(IFinanceFactory financeFactory){
        _financeFactory = financeFactory;
    }

    public Category CreateCategory(bool type, string name){
        Guid guid_id = Guid.NewGuid();
        Category category = _financeFactory.CreateCategory(guid_id, type, name);
        _categories.Add(category);
        return category;
    }

    public void AddCategory(Guid guid_id, bool type, string name){
        if (GetCategory(guid_id) == null){
            Category category = _financeFactory.CreateCategory(guid_id, type, name);
            _categories.Add(category);
        } else {
            throw new ArgumentException("Категория с таким id уже существует");
        }
        
    }

    public Category? GetCategory(Guid categoryId)
    {
        return _categories.FirstOrDefault(c => c.Id == categoryId);
    }
    
    public List<Category> GetCategories()
    {
        return _categories;
    }

    public void ChangeCategory(Guid categoryId, string name)
    {
        Category? category = GetCategory(categoryId);
        if (category != null){
            category.ChangeName(name);
            Console.WriteLine($"Категория с id {categoryId} изменена.");
        } else {
            Console.WriteLine($"Категория с id {categoryId} не найдена.");
        }
        
    }

    public bool DeleteCategory(Guid categoryId){
        Category? category = GetCategory(categoryId);
        if (category != null)
        {
            _categories.Remove(category); // Удаляем из списка
            Console.WriteLine($"Категория с id {categoryId} удалена.");
            return true; // Возвращаем true, если удаление прошло успешно
        }
        else
        {
            Console.WriteLine($"Категория с id {categoryId} не найдена.");
            return false; // Возвращаем false, если не найдена
        }
    }
}