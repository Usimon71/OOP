using Microsoft.Extensions.DependencyInjection;
using Presentation.Console.AdminLogin;
using Presentation.Console.CheckBalance;
using Presentation.Console.GetOperations;
using Presentation.Console.Login;
using Presentation.Console.Logout;
using Presentation.Console.Replenish;
using Presentation.Console.Withdraw;

namespace Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();

        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ReplenishScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CheckBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetOperationsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();

        return collection;
    }
}
