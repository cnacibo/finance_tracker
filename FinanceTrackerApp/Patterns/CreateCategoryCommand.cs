namespace FinanceTrackerApp;

public class CreateCategoryCommand : ICommand{
    private readonly CategoryFacade _categoryFacade;
    private readonly string _type;
    private readonly string _name;

    public CreateCategoryCommand(CategoryFacade categoryFacade, string type, string name){
        _categoryFacade = categoryFacade;
        _type = type;
        _name = name;
    }

    public override void Execute() {
        _categoryFacade.CreateCategory(_type, _name);
    }

    public override string ToString() => "Создана новая категория операций.";

}