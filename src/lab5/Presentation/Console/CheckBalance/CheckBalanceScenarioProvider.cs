using Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.CheckBalance;

public class CheckBalanceScenarioProvider : IScenarioProvider
{
    private readonly IAccountOperationsService _accountOperationsService;
    private readonly ICurrentAccountService _currentAccountService;

    public CheckBalanceScenarioProvider(
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

        scenario = new CheckBalanceScenario(_accountOperationsService);

        return true;
    }
}
