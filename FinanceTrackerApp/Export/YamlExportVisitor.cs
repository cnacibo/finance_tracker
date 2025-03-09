namespace FinanceTrackerApp;
using YamlDotNet.Serialization;
using System.Globalization;
using System.IO;

// public class YamlExportVisitor : IExportVisitor{
//     public void Visit(){
//         var serializer = new SerializerBuilder().Build();
//         File.WriteAllText("export.yaml", serializer.Serialize(new { accounts, categories, operations }));
//     }
// }