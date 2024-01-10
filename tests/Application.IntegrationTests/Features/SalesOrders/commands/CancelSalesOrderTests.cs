using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Items.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Application.UseCases.SalesOrders.Commands.Cancel;
using DddEf.Domain.Common.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using FluentAssertions;
using NUnit.Framework;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;

namespace DddEf.Application.IntegrationTests.Features.SalesOrders.commands;

using static Testing;

public class CancelSalesOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCancelCustomer()
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
            new List<SalesOrderItemVm>
            {
                new SalesOrderItemVm(itemId1,1,1000),
                new SalesOrderItemVm(itemId2,2,2000)
            }
        );



        var salesOrderId = await SendAsync(createSalesOrderCommand);


        var cancelSalesOrderCommand = new CancelSalesOrderCommand
        (
           salesOrderId
        );

        // Act
        await SendAsync(cancelSalesOrderCommand);


        // Assert
        var salesOrder = await FindAsync<SalesOrder>(new SalesOrderId(salesOrderId));

        salesOrder.Should().NotBeNull();
    }
}
