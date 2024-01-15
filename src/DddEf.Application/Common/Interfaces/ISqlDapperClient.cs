using System.Data;
using static Dapper.SqlMapper;

namespace DddEf.Application.Common.Interfaces;

public interface ISqlDapperClient
{
    Task<int> ExecuteAsync(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<T> QueryMultipleAsync<T>(string sql, Func<GridReader, Task<T>> iduConvert, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, CancellationToken cancellationToken = new(), IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
}