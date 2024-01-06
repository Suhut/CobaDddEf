using DddEf.Application.Common;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace DddEf.Infrastructure.Persistence
{
    public class DddEfContext : DbContext, IDddEfContext
    {
       
        public DddEfContext(
            DbContextOptions options) : base(options)
        { 
        }
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<SalesOrder> SalesOrders { get; set; } = null!;
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; } = null!;


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        { 
            return await base.SaveChangesAsync(cancellationToken); 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }


        public Task<Unit> ExecuteRawSql(string ssql, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Unit> ExecuteRawSql(string ssql, object param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalarRawSql<T>(string ssql, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteScalarRawSql<T>(string ssql, object param, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync<TEntity>(Guid? kayValue, CancellationToken cancellationToken) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}
