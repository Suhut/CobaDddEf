using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class ItemConfiguration : AggregateRootConfiguration<Item>
{ 
    public override void Configure(EntityTypeBuilder<Item> builder)
    {
        base.Configure(builder);
        ConfigurationItemsTable(builder);
    }

    private void ConfigurationItemsTable(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Tm_Item");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new ItemId(value)
                )
                ;

        builder.Property(m => m.ItemCode)
            .HasMaxLength(50);

        builder.Property(m => m.ItemName)
            .HasMaxLength(300); 

    }
}

