using DddEf.Application.Common.Abstractions;
using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DddEf.Infrastructure.Persistence
{
    public class DddEfContext : DbContext, IDddEfContext
    {

        private readonly IDateTimeProvider _dateTimeProvider;
        public DddEfContext(
            DbContextOptions options,
            IDateTimeProvider dateTimeProvider
            ) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<SalesOrder> SalesOrders { get; set; } = null!;
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; } = null!;


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
            {
                if ((entry.State == EntityState.Modified))
                {
                    entry.Entity.IncreaseVersion();
                }

                //last change
                switch (entry.State)
                {
                    case EntityState.Added: 
                        entry.Entity.SetCreatedDateOffset(_dateTimeProvider.Now);
                        break;
                    case EntityState.Modified:
                        entry.Entity.SetModifiedDateOffset(_dateTimeProvider.Now);
                        break;
                }


            }
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw;
            }

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
