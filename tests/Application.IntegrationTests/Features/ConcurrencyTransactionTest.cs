﻿using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Items.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Domain.Common.ValueObjects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;

namespace DddEf.Application.IntegrationTests.Features.SalesOrders;

using static Testing;

public class ConcurrencyTransactionTest : BaseTestFixture
{
    [Test]
    public async Task ShouldError()
    {
        // Arrange
        var addCustomerCommand = new AddCustomerCommand
        (
            "CustomerCode01",
            "CustomerName01"
        );

        var customerId = await SendAsync(addCustomerCommand);

        var addItemCommand1 = new AddItemCommand
        (
            "ItemCode001",
            "ItemName001"
        );
        var itemId1 = await SendAsync(addItemCommand1);

        var addItemCommand2 = new AddItemCommand
        (
            "ItemCode002",
            "ItemName002"
        );
        var itemId2 = await SendAsync(addItemCommand2);


        var createSalesOrderCommand = new AddSalesOrderCommand
        (
            "Trans001",
            DateTime.Now.Date,
            customerId,
            new Address("Blora", "Indonesia"),
            new Address("Jakarta", "Indonesia"),
            new List<AddSalesOrderItemVm>
            {
                new AddSalesOrderItemVm(itemId1,1,1000),
                new AddSalesOrderItemVm(itemId2,2,2000)
            },
            []
        );



        var salesOrderId = await SendAsync(createSalesOrderCommand);


        var entity1 = await FindAsync<SalesOrder>(new SalesOrderId(salesOrderId));
        var entity2 = await FindAsync<SalesOrder>(new SalesOrderId(salesOrderId));


        entity1.Close();
        await UpdateAsync(entity1);

        // Act  
        entity2.Cancel();

        // Assert
        FluentActions.Invoking(() =>
               UpdateAsync(entity2)).Should().ThrowAsync<DbUpdateConcurrencyException>();
    }
}
