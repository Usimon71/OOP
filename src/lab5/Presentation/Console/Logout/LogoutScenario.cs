using Application.Contracts.Accounts;
using Spectre.Console;

namespace Presentation.Console.Logout;

public class LogoutScenario : IScenario
{
    private readonly IAccountOperationsService _accountOperationsService;

    public LogoutScenario(IAccountOperationsService accountOperationsService)
    {
        _accountOperationsService = accountOperationsService;
    }

    public string Name => "Logout";

    public void Run()
    {
        _accountOperationsService.Logout();

        AnsiConsole.WriteLine("Successfully logged out");
        AnsiConsole.Ask<string>("Ok");
    }
}
