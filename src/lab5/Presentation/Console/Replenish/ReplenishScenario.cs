using Application.Contracts.Accounts;
using Application.Contracts.Results;
using Spectre.Console;

namespace Presentation.Console.Replenish;

public class ReplenishScenario : IScenario
{
    private readonly IAccountOperationsService _accountOperationsService;

    public ReplenishScenario(
        IAccountOperationsService accountOperationsService)
    {
        _accountOperationsService = accountOperationsService;
    }

    public string Name => "Replenish balance";

    public void Run()
    {
        long amount = AnsiConsole.Ask<long>("Deposit your money: ");

        ReplenishResult result = _accountOperationsService.ReplenishAccountNumber(amount);

        string message = result switch
        {
            ReplenishResult.Success => "Successful replenishment",
            ReplenishResult.AccountNotFound => "Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}
