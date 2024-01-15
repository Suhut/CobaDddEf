using Microsoft.Data.SqlClient;

namespace DddEf.Infrastructure.Dapper;

public class SqlConnectionDelegate
{
    public delegate SqlConnection SqlConnectionFactory();
}
