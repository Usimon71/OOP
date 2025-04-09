using Application.Contracts.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Login;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IAccountOperationsService _service;
    private readonly ICurrentAccountService _currentAccount;

    public LoginScenarioProvider(
        IAccountOperationsService service,
        ICurrentAccountService currentAccount)
    {
        _service = service;
        _currentAccount = currentAccount;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccount.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginScenario(_service);
        return true;
    }
}
