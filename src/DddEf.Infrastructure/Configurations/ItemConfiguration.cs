using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{ 
    public void Configure(EntityTypeBuilder<Item> builder)
    {
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
                value => ItemId.Create(value)
                )
                ;

        builder.Property(m => m.ItemCode)
            .HasMaxLength(50);

        builder.Property(m => m.ItemName)
            .HasMaxLength(300);


        //builder.Metadata.FindNavigation(nameof(Item))!
        //    .SetPropertyAccessMode(PropertyAccessMode.Field)
        //    ;

        builder.Property("CreatedDateOffset");

        builder.Property("ModifiedDateOffset");

    }
}

