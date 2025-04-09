using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Login;

public class LoginScenario : IScenario
{
    private readonly IAccountOperationsService _accountOperationsService;

    public LoginScenario(IAccountOperationsService accountOperationsService)
    {
        _accountOperationsService = accountOperationsService;
    }

    public string Name => "Login in account";

    public void Run()
    {
        long accountNumber = AnsiConsole.Ask<long>($"Enter account number: ");
        int pin = AnsiConsole.Ask<int>($"Enter pin code: ");

        LoginResult result = _accountOperationsService.Login(accountNumber, pin);

        string message = result switch
        {
            LoginResult.Success => "Successful login!",
            LoginResult.WrongPin => "Wrong pin code!",
            LoginResult.AccountNotFound => "Account not found!",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}
