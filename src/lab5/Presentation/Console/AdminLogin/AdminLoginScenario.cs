using Application.Contracts.Admins;
using Spectre.Console;

namespace Presentation.Console.AdminLogin;

public class AdminLoginScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminLoginScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Admin Login";

    public void Run()
    {
        string systemPassword = AnsiConsole.Ask<string>("Enter your admin password: ");

        AdminLoginResult result = _adminService.Login(systemPassword);

        string message = result switch
        {
            AdminLoginResult.Success => "Successful login",
            AdminLoginResult.WrongPassword => "Wrong password",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}
