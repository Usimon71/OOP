using Application.Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

public class SystemRepository : ISystemRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public SystemRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public int? GetPasswordHash()
    {
        const string sql = """
                           select password_hash
                           from password;
                           """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);

        return (int?)command.ExecuteScalar();
    }
}
