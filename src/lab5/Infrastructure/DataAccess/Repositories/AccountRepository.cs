using Application.Abstractions.Repositories;
using Application.Models.Accounts;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void SetBalanceForAccount(long number, long? value)
    {
        if (value is null) return;

        const string sql = """
                           update accounts
                           set balance = :value
                           where number = :number;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("value", value)
            .AddParameter("number", number);

        command.ExecuteNonQuery();
    }

    public Account? FindAccountByNumber(long number)
    {
        const string sql = """
                           select account_id, number, pin, balance
                           from accounts
                           where number = :number;
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("number", number);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Account(
            Id: reader.GetInt64(0),
            Number: reader.GetInt64(1),
            Pin: reader.GetInt32(2),
            Balance: reader.GetInt64(3));
    }

    public long? FindBalanceByAccountNumber(long number)
    {
        const string sql = """
                           select balance
                           from accounts
                           where number = :number;
                           """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("number", number);

        return (long?)command.ExecuteScalar();
    }
}
