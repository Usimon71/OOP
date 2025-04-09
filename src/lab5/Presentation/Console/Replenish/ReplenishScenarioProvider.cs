using Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Replenish;

public class ReplenishScenarioProvider : IScenarioProvider
{
    private readonly IAccountOperationsService _accountOperationsService;
    private readonly ICurrentAccountService _currentAccountService;

    public ReplenishScenarioProvider(
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

        scenario = new ReplenishScenario(_accountOperationsService);

        return true;
    }
}
