using Application.Models.Operations;
using Itmo.Dev.Platform.Postgres.Plugins;
using Npgsql;

namespace Infrastructure.DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<OperationType>();
    }
}
