﻿using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddEf.Infrastructure.Configurations;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        ConfigurationSalesOrdersTable(builder); 
        ConfigurationSalesOrderItemsTable(builder);
    }

  
    private void ConfigurationSalesOrdersTable(EntityTypeBuilder<SalesOrder> builder)
    {
        builder.ToTable("Tx_SalesOrder");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SalesOrderId.Create(value)
                )
                ;


        builder.Property(m => m.CustomerId)
           .HasConversion(
               id => id.Value,
               value => CustomerId.Create(value)
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


        ////ValueObject as different table
        //builder.OwnsOne(
        //   salesOrder => salesOrder.BillAddress,
        //   sb =>
        //   {
        //       sb
        //           .ToTable("Tx_SalesOrder__BillAddress");

        //       sb
        //           .Property(street => street.City)
        //           .HasMaxLength(50)
        //           ;

        //       sb
        //           .Property(street => street.Country)
        //           .HasMaxLength(50)
        //           ;

        //       sb
        //           .WithOwner()
        //           .HasForeignKey("Id");

        //       sb
        //           .Property<Guid>("DetId");

        //       sb
        //           .HasKey("DetId");

        //   });



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

            sb.Property(m => m.ProductId)
               .HasConversion(
                   productId => productId.Value,
                   value => ProductId.Create(value)
                   )
                   ;

        });
         

        builder.Navigation(s => s.Items).Metadata.SetField("_items");
        builder.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        

    }

}

