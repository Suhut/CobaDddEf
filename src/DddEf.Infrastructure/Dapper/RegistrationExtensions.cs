using DddEf.Application.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using static DddEf.Infrastructure.Dapper.SqlConnectionDelegate;

namespace DddEf.Infrastructure.Dapper;

internal static class RegistrationExtensions
{
    internal static void AddSqlDapperClient(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<SqlConnectionFactory>(() => new SqlConnection(connectionString));
        services.AddScoped(_ => SqlResiliencyPolicy.GetSqlResiliencyPolicy());
        services.AddScoped<ISqlDapperClient, SqlDapperClient>();
    }
}