namespace FinanceTrackerApp;

public class CreateCategoryCommand : ICommand{
    private readonly CategoryFacade _categoryFacade;
    private readonly bool _type;
    private readonly string _name;

    public CreateCategoryCommand(CategoryFacade categoryFacade, bool type, string name){
        _categoryFacade = categoryFacade;
        _type = type;
        _name = name;
    }

    public override void Execute() {
        _categoryFacade.CreateCategory(_type, _name);
    }

    public override string ToString() => "Создана новая категория операций.";

}