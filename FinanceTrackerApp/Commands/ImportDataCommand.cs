using System;

namespace FinanceTrackerApp;

public class ImportDataCommand : ICommand
{
    private readonly string _filePath;
    private readonly DataImporter _importer;
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;

    public ImportDataCommand(string filePath, DataImporter importer, BankAccountFacade bankAccountFacade, 
    CategoryFacade categoryFacade, OperationFacade operationFacade)
    {
        _filePath = filePath;
        _importer = importer;
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
    }

    public override void Execute()
    {
        Console.WriteLine("\nНачинаем читать файл...");
        _importer.ImportData(_filePath, _bankAccountFacade, _categoryFacade, _operationFacade);
    }

    public override string ToString() => $"Импорт данных из {_filePath}";
}
