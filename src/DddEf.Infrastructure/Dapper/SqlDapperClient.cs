using Dapper;
using DddEf.Application.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using System.Data;
using System.Runtime.CompilerServices;
using static Dapper.SqlMapper;
using static DddEf.Infrastructure.Dapper.ContextHelper;
using static DddEf.Infrastructure.Dapper.SqlConnectionDelegate;

namespace DddEf.Infrastructure.Dapper;


public class SqlDapperClient : ISqlDapperClient
{
    private readonly ILogger<SqlDapperClient> logger;
    private readonly SqlConnectionFactory connectionFactory;
    private readonly IAsyncPolicy resiliencyPolicy;

    public SqlDapperClient(
        ILogger<SqlDapperClient> logger,
        SqlConnectionFactory connectionFactory,
        IAsyncPolicy resiliencyPolicy)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        this.resiliencyPolicy = resiliencyPolicy ?? throw new ArgumentNullException(nameof(resiliencyPolicy));
    }

    public Task<int> ExecuteAsync(string sql, object param = null, CancellationToken cancellationToken=new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        => ExecuteWithResiliency((s, p, c) => c.ExecuteAsync(s, p, transaction, commandTimeout, commandType), sql, param, cancellationToken: cancellationToken);

    public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        => ExecuteWithResiliency((s, p, c) => c.ExecuteScalarAsync<T>(s, p, transaction, commandTimeout, commandType), sql, param, cancellationToken: cancellationToken);

    public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        => ExecuteWithResiliency((s, p, c) => c.QueryFirstOrDefaultAsync<T>(s, p, transaction, commandTimeout, commandType), sql, param, cancellationToken: cancellationToken);

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        => ExecuteWithResiliency((s, p, c) => c.QueryAsync<T>(s, p, transaction, commandTimeout, commandType), sql, param,   cancellationToken: cancellationToken);
 
    public Task<T> QueryMultipleAsync<T>(string sql, Func<GridReader, Task<T>> IduConvert, object param = null,  CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
         => ExecuteWithResiliency(async (s, p, c) =>
        {  
            var objWithDetails = await c.QueryMultipleAsync(s, p, transaction, commandTimeout, commandType); 
            return await IduConvert(objWithDetails); 
        }, sql, param, cancellationToken: cancellationToken);
     
    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        => ExecuteWithResiliency((s, p, c) => c.QueryAsync(s, map, p, transaction, buffered, splitOn, commandTimeout, commandType), sql, param, cancellationToken: cancellationToken);

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        => ExecuteWithResiliency((s, p, c) => c.QueryAsync(s, map, p, transaction, buffered, splitOn, commandTimeout, commandType), sql, param, cancellationToken: cancellationToken);

    private async Task<T> ExecuteWithResiliency<T>(Func<string, object, SqlConnection, Task<T>> connectionFunc, string sql, object param = null, CancellationToken cancellationToken = new(), [CallerMemberName] string operation = "")
    {
        using var connection = connectionFactory();
        return await resiliencyPolicy.ExecuteAsync(
            ctx => connectionFunc(sql, param, connection), 
            NewContext(connection, logger, sql, param, operation)

            );
    }

   
}
