using Application.Abstractions.Repositories;
using Infrastructure.DataAccess.Plugins;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IAccountRepository, AccountRepository>();
        collection.AddScoped<IOperationRepository, OperationRepository>();
        collection.AddScoped<ISystemRepository, SystemRepository>();

        return collection;
    }
}
