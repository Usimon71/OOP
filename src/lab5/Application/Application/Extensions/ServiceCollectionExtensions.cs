using Application.Application.Accounts;
using Application.Application.Admins;
using Application.Contracts.Accounts;
using Application.Contracts.Admins;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AccountService>();
        services.AddScoped<IAccountOperationsService, AccountOperationsService>();

        services.AddScoped<CurrentAccountManager>();
        services.AddScoped<ICurrentAccountService>(
            provider => provider.GetRequiredService<CurrentAccountManager>());

        services.AddScoped<IAdminService, AdminService>();

        return services;
    }
}
