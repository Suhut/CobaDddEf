using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class CustomerConfiguration : AggregateRootConfiguration<Customer> 
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        ConfigurationCustomersTable(builder);
    }

    private void ConfigurationCustomersTable(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Tm_Customer");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value)
                )
                ;

        builder.Property(m => m.CustomerCode)
            .HasMaxLength(50);

        builder.Property(m => m.CustomerName)
            .HasMaxLength(300); 

    }
}

