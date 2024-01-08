﻿using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Items.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Application.UseCases.SalesOrders.Commands.Close;
using DddEf.Domain.Common.ValueObjects; 

using FluentAssertions;
using NUnit.Framework;
using DddEf.Domain.Aggregates.SalesOrder;

namespace DddEf.Application.IntegrationTests.Features.SalesOrders.commands;

using static Testing;

public class CloseSalesOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCloseCustomer()
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
        var productId1 = await SendAsync(addItemCommand1);

        var addItemCommand2 = new AddItemCommand
        (
            "ItemCode002",
            "ItemName002"
        );
        var productId2 = await SendAsync(addItemCommand2);


        var createSalesOrderCommand = new AddSalesOrderCommand
        (
            "Trans001",
            DateTime.Now.Date,
            customerId,
            new Address("Blora", "Indonesia"),
            new Address("Jakarta", "Indonesia"),
            new List<SalesOrderItemVm>
            {
                new SalesOrderItemVm(productId1,1,1000),
                new SalesOrderItemVm(productId2,2,2000)
            }
        );



        var salesOrderId = await SendAsync(createSalesOrderCommand);


        var closeSalesOrderCommand = new CloseSalesOrderCommand
        (
           salesOrderId
        );

        // Act
        await SendAsync(closeSalesOrderCommand);


        // Assert
        var salesOrder = await FindAsync<SalesOrder>(salesOrderId);

        salesOrder.Should().NotBeNull();
    }
}
