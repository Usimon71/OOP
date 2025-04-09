using Application.Abstractions.Repositories;
using Application.Models.Operations;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.DataAccess.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public OperationRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<Operation>? FindOperationsByAccountNumber(long accountNumber)
    {
        const string sql = """
                           select operation_id, account_number, operation_type, timestamp 
                           from operations
                           where account_number = :accountNumber
                           order by timestamp desc;
                           """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("accountNumber", accountNumber);

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Operation(
                Id: reader.GetGuid(0),
                AccountNumber: reader.GetInt64(1),
                OperationType: reader.GetFieldValue<OperationType>(2),
                Timestamp: reader.GetDateTime(3));
        }
    }

    public AddingRecordResult AddOperation(Operation operation)
    {
        const string sql = """
                           insert into operations (operation_id, account_number, operation_type)
                           values (:operation_id, :account_number, :operation_type);
                           """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("operation_id", operation.Id)
            .AddParameter("account_number", operation.AccountNumber)
            .AddParameter("operation_type", operation.OperationType);

        command.ExecuteNonQuery();
        return new AddingRecordResult.Success();
    }
}
