using System;

namespace FinanceTrackerApp;

public class ExportDataCommand : ICommand
{
    private readonly string _fileType;
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;

    public ExportDataCommand(string fileType, BankAccountFacade bankAccountFacade, 
    CategoryFacade categoryFacade, OperationFacade operationFacade)
    {
        _fileType = fileType.ToLower(); // Приводим к нижнему регистру для удобства
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
    }

    public override void Execute()
    {
        IExportVisitor exportVisitor;
        Console.WriteLine("\nНачинаем записывать в файл...");
        
        // Выбираем нужный экспортёр в зависимости от формата файла
        switch (_fileType)
        {
            case "json":
                exportVisitor = new JsonExportVisitor();
                break;
            case "csv":
                exportVisitor = new CsvExportVisitor();
                break;
            case "yaml":
                exportVisitor = new YamlExportVisitor();
                break;
            default:
                Console.WriteLine("Ошибка: Неподдерживаемый формат файла!");
                return;
        }
        try{
            exportVisitor.Visit(_bankAccountFacade, _categoryFacade, _operationFacade);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Данные успешно экспортированы в {_fileType.ToUpper()}!");
            Console.ResetColor();
        } catch (Exception e){
            Console.WriteLine("Ошибка записи в файл: ", e.Message);
        }


    }

    public override string ToString() => $"Экспорт данных в {_fileType.ToUpper()}";
}
