namespace FinanceTrackerApp;
using System.Diagnostics;


public class TimedCommand : ICommand{
    private readonly ICommand _command;

    public TimedCommand(ICommand command){
        _command = command;
    }

    public override void Execute() {
       Stopwatch stopwatch = Stopwatch.StartNew();
        _command.Execute();
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
    }

}