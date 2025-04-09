using Application.Contracts.Accounts;
using Application.Contracts.Admins;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.AdminLogin;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAccountService _currentAccount;

    public AdminLoginScenarioProvider(
        IAdminService adminService,
        ICurrentAccountService currentAccount)
    {
        _adminService = adminService;
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

        scenario = new AdminLoginScenario(_adminService);
        return true;
    }
}
