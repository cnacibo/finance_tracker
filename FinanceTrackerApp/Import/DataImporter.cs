namespace FinanceTrackerApp;

public abstract class DataImporter{

    protected abstract void ParseData(string data, List<BankAccount> accounts, List<Category> categories, List<Operation> operations);
    public void ImportData(string filePath, List<BankAccount> accounts, List<Category> categories, List<Operation> operations){
        var rawData = File.ReadAllText(filePath);
        ParseData(rawData, accounts, categories, operations);
    }
}