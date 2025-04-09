using Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Withdraw;

public class WithdrawScenarioProvider : IScenarioProvider
{
    private readonly IAccountOperationsService _accountOperationsService;
    private readonly ICurrentAccountService _currentAccountService;

    public WithdrawScenarioProvider(
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

        scenario = new WithdrawScenario(_accountOperationsService);

        return true;
    }
}
