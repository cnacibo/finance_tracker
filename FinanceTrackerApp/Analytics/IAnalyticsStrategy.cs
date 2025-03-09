using System.Runtime.Serialization;

namespace FinanceTrackerApp;

public interface IAnalyticsStrategy{

    object Analyze(List<Operation> operations);
}