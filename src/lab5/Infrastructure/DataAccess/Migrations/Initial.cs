using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create type operation_type as enum
        (
            'creation',
            'balance_check',
            'withdraw',
            'replenish'
        );
        
        create table accounts
        (
            account_id bigint primary key generated always as identity ,
            number bigint not null unique,
            pin integer not null ,
            balance bigint not null
        );
        
        create table operations
        (
            operation_id uuid primary key ,
            account_number bigint not null ,
            operation_type operation_type not null,
            timestamp timestamp default current_timestamp
        );
        
        create table password
        (
            password_hash integer not null
        );
        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table accounts;
    drop table operations;
    drop table password;
    
    drop type operation_type;
    """;
}
