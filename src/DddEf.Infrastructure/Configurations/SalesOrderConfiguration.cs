using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DddEf.Infrastructure.Configurations;

public class SalesOrderConfiguration : AggregateRootConfiguration<SalesOrder>
{
    public override void Configure(EntityTypeBuilder<SalesOrder> builder)
    { 
        base.Configure(builder);
        ConfigurationSalesOrdersTable(builder); 
        ConfigurationSalesOrderItemsTable(builder);
        ConfigurationSalesOrderItemSecondsTable(builder);
    }

  
    private void ConfigurationSalesOrdersTable(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("Tx_SalesOrder");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new SalesOrderId(value)
                )
                ;


        builder.Property(m => m.CustomerId)
           .HasConversion(
               id => id.Value,
               value => new CustomerId(value)
               )
               ;

        //ValueObject as same table
        builder.OwnsOne(
           salesOrder => salesOrder.ShipAddress,
           sb =>
           {
               sb
                   .Property(street => street.City)
                   .HasMaxLength(50)
                   ;

               sb
                   .Property(street => street.Country)
                   .HasMaxLength(50)
                   ;
           });

        //ValueObject as same table
        builder.OwnsOne(
           salesOrder => salesOrder.BillAddress,
           sb =>
           {
               sb
                   .Property(street => street.City)
                   .HasMaxLength(50)
                   ;

               sb
                   .Property(street => street.Country)
                   .HasMaxLength(50)
                   ;
           }); 

        builder.Property("CreatedDateOffset");

        builder.Property("ModifiedDateOffset");

    }


    private void ConfigurationSalesOrderItemsTable(EntityTypeBuilder<SalesOrder> builder)
    {
       builder.OwnsMany(m => m.Items, sb =>
        {
            sb.ToTable("Tx_SalesOrder_Item");
             
            sb.WithOwner().HasForeignKey("Id"); 
              
            sb.HasIndex("Id", "RowNumber").IsUnique();
             
            sb.HasKey("DetId"); 

            sb.Property(m => m.ItemId)
               .HasConversion(
                   itemId => itemId.Value,
                   value => new ItemId(value)
                   )
                   ; 

            ConfigurationSalesOrderItemBinsTable(sb); 

        });
         

        builder.Navigation(s => s.Items).Metadata.SetField("_items");
        builder.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);

         

    }

    private void ConfigurationSalesOrderItemBinsTable(OwnedNavigationBuilder<SalesOrder, SalesOrderItem> builder)
    {
        builder.OwnsMany(m => m.Bins, sb =>
        {
            sb.ToTable("Tx_SalesOrder_Item_Bin");

            sb.WithOwner().HasForeignKey("DetId");

            sb.HasIndex("DetId", "RowNumber").IsUnique();

            sb.HasKey("DetDetId");

        });

        builder.Navigation(s => s.Bins).Metadata.SetField("_bins");
        builder.Navigation(s => s.Bins).UsePropertyAccessMode(PropertyAccessMode.Field);


    }


    private void ConfigurationSalesOrderItemSecondsTable(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.OwnsMany(m => m.ItemSeconds, sb =>
        {
            sb.ToTable("Tx_SalesOrder_ItemSecond");

            sb.WithOwner().HasForeignKey("Id");

            sb.HasIndex("Id", "RowNumber").IsUnique();

            sb.HasKey("DetId");

            sb.Property(m => m.ItemId)
               .HasConversion(
                   itemId => itemId.Value,
                   value => new ItemId(value)
                   )
                   ;


            ConfigurationSalesOrderItemSecondBinsTable(sb);
        });


        builder.Navigation(s => s.ItemSeconds).Metadata.SetField("_itemSeconds");
        builder.Navigation(s => s.ItemSeconds).UsePropertyAccessMode(PropertyAccessMode.Field);


    }

    private void ConfigurationSalesOrderItemSecondBinsTable(OwnedNavigationBuilder<SalesOrder, SalesOrderItemSecond> builder)
    {
        builder.OwnsMany(m => m.Bins, sb =>
        {
            sb.ToTable("Tx_SalesOrder_ItemSecond_Bin");

            sb.WithOwner().HasForeignKey("DetId");

            sb.HasIndex("DetId", "RowNumber").IsUnique();

            sb.HasKey("DetDetId");
             

        });

        builder.Navigation(s => s.Bins).Metadata.SetField("_bins");
        builder.Navigation(s => s.Bins).UsePropertyAccessMode(PropertyAccessMode.Field);


    }

}

