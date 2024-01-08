﻿using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Items.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Common.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.SalesOrders.commands;

using static Testing;

public class AddSalesOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateSalesOrder()
    {
        // Arrange
        var createCustomerCommand = new CreateCustomerCommand
        (
            "CustomerCode01",
            "CustomerName01"
        );

        var customerId = await SendAsync(createCustomerCommand);

        var createItemCommand1 = new CreateItemCommand
        (
            "ItemCode001",
            "ItemName001"
        );
        var productId1 = await SendAsync(createItemCommand1);

        var createItemCommand2 = new CreateItemCommand
        (
            "ItemCode002",
            "ItemName002"
        );
        var productId2 = await SendAsync(createItemCommand2);


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


        // Act
        var salesOrderId = await SendAsync(createSalesOrderCommand);


        // Assert
        var salesOrder = await FindAsync<SalesOrder>(salesOrderId);

        salesOrder.Should().NotBeNull();
        //salesOrder!.TransNo.Should().Be(createSalesOrderCommand.TransNo);
        //salesOrder.TransDate.Should().Be(createSalesOrderCommand.TransDate);
        //salesOrder.Status.Should().Be("Open");
        //salesOrder.ShipAddress.Country.Should().Be(createSalesOrderCommand.ShipAddress.Country);
        //salesOrder.ShipAddress.City.Should().Be(createSalesOrderCommand.ShipAddress.City);
        //salesOrder.BillAddress.Country.Should().Be(createSalesOrderCommand.BillAddress.Country);
        //salesOrder.BillAddress.City.Should().Be(createSalesOrderCommand.BillAddress.City);
        //salesOrder.Items.Should().NotBeNull();
        //salesOrder.Items.Count.Should().Be(createSalesOrderCommand.Items.Count);
    }
}