namespace FinanceTrackerApp;

public class Category{
    public Guid Id { get; } = Guid.NewGuid();
    public bool Type { get; set; } // true - gain, false - lose 
    public string Name { get; set; }

    public Category(bool type, string name){
        Type = type;
        Name = name;
    }

    public void ChangeName(string newName){
        Name = newName;
    }

}