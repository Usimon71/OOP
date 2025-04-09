using Application.Contracts.Accounts;
using Application.Contracts.Results;
using Spectre.Console;

namespace Presentation.Console.Withdraw;

public class WithdrawScenario : IScenario
{
    private readonly IAccountOperationsService _accountOperationsService;

    public WithdrawScenario(IAccountOperationsService accountOperationsService)
    {
        _accountOperationsService = accountOperationsService;
    }

    public string Name => "Withdraw money";

    public void Run()
    {
        long amount = AnsiConsole.Ask<long>("Enter the amount of money: ");

        WithdrawResult result = _accountOperationsService.WithdrawFromAccountNumber(amount);

        string message = result switch
        {
            WithdrawResult.Success => "Get your money",
            WithdrawResult.InsufficientFunds => "Insufficient funds",
            WithdrawResult.AccountNotFound => "Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}
