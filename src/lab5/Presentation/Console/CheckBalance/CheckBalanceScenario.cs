using Application.Contracts.Accounts;
using Application.Contracts.Results;
using Spectre.Console;

namespace Presentation.Console.CheckBalance;

public class CheckBalanceScenario : IScenario
{
    private readonly IAccountOperationsService _accountOperationsService;

    public CheckBalanceScenario(IAccountOperationsService accountOperationsService)
    {
        _accountOperationsService = accountOperationsService;
    }

    public string Name => "Check Balance";

    public void Run()
    {
        BalanceCheckResult result = _accountOperationsService.BalanceCheckAccountNumber();

        string message = result switch
        {
            BalanceCheckResult.Success success => $"Your balance is {success.Balance}",
            BalanceCheckResult.NotFound => "Account is not found.",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}
