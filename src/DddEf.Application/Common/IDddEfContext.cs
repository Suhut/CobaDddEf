using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DddEf.Application.Common;

public interface IDddEfContext
{
    DbSet<Customer> Customers { get; set; } 
    DbSet<Product> Products { get; set; }  
    DbSet<SalesOrder> SalesOrders { get; set; }  
    DbSet<SalesOrderItem> SalesOrderItems { get; set; }   
    Task<TEntity> FindAsync<TEntity>(Guid? kayValue, CancellationToken cancellationToken) where TEntity : class; 
    Task<Unit> ExecuteRawSql(string ssql, CancellationToken cancellationToken);
    Task<Unit> ExecuteRawSql(string ssql, object param, CancellationToken cancellationToken);
    Task<T> ExecuteScalarRawSql<T>(string ssql, CancellationToken cancellationToken);
    Task<T> ExecuteScalarRawSql<T>(string ssql, object param, CancellationToken cancellationToken);  
    Task<int> SaveChangesAsync(CancellationToken cancellationToken); 
}

