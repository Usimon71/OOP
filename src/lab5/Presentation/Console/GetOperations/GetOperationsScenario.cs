using Application.Contracts.Accounts;
using Application.Models.Operations;
using Spectre.Console;

namespace Presentation.Console.GetOperations;

public class GetOperationsScenario : IScenario
{
    private readonly IAccountOperationsService _accountOperationsService;

    public GetOperationsScenario(IAccountOperationsService accountOperationsService)
    {
        _accountOperationsService = accountOperationsService;
    }

    public string Name => "Get operations history.";

    public void Run()
    {
        IEnumerable<Operation> result = _accountOperationsService.GetOperations();

        IEnumerable<Operation> operations = result.ToList();
        if (!operations.Any())
        {
            AnsiConsole.WriteLine("No operations found.");
            AnsiConsole.Ask<string>("Ok");

            return;
        }

        foreach (Operation operation in operations)
        {
            AnsiConsole.WriteLine(
                "Id: " + operation.Id + " | " +
                " Type: " + operation.OperationType + "\t | " +
                " Timestamp: " + operation.Timestamp);
        }

        AnsiConsole.Ask<string>("Ok");
    }
}
