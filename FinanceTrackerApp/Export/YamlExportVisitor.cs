using System;
namespace FinanceTrackerApp;
using YamlDotNet.Serialization;
using System.Globalization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;

public class YamlExportVisitor : IExportVisitor{
    public void Visit(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade){
        var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // Указываем стиль именования
                .Build();

            var data = new
            {
                accounts = accountFacade.GetAccounts(),
                categories = categoryFacade.GetCategories(),
                operations = operationFacade.GetOperations()
            };
        File.WriteAllText("export.yaml", serializer.Serialize(data));
    }
}