using Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Logout;

public class LogoutScenarioProvider : IScenarioProvider
{
    private readonly IAccountOperationsService _accountOperationsService;
    private readonly ICurrentAccountService _currentAccountService;

    public LogoutScenarioProvider(
        IAccountOperationsService accountOperationsService,
        ICurrentAccountService currentAccountService)
    {
        _accountOperationsService = accountOperationsService;
        _currentAccountService = currentAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is null)
        {
            scenario = null;

            return false;
        }

        scenario = new LogoutScenario(_accountOperationsService);

        return true;
    }
}
