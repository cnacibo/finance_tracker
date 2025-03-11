using System;
namespace FinanceTrackerApp;


public class Category{
    public Guid Id { get; }
    public bool Type { get; private set; } // true - gain, false - lose 
    public string Name { get; private set; }

    public Category(Guid id, bool type, string name){
        Id = id;
        Type = type;
        Name = name;
    }

    public void ChangeName(string newName){
        Name = newName;
    }

}