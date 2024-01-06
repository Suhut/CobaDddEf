using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Products.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Common.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.SalesOrders.commands;

using static Testing;

public class RemoveLineSalesOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRemoveLineCustomer()
    {
        // Arrange
        var createCustomerCommand = new CreateCustomerCommand
        (
            "CustomerCode01",
            "CustomerName01"
        );

        var customerId = await SendAsync(createCustomerCommand);

        var createProductCommand1 = new CreateProductCommand
        (
            "ProductCode001",
            "ProductName001"
        );
        var productId1 = await SendAsync(createProductCommand1);

        var createProductCommand2 = new CreateProductCommand
        (
            "ProductCode002",
            "ProductName002"
        );
        var productId2 = await SendAsync(createProductCommand2);


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


        var cancelSalesOrderCommand = new RemoveLineSalesOrderCommand
        (
           salesOrderId
        );

        // Act
        await SendAsync(cancelSalesOrderCommand);


        // Assert
        var salesOrder = await FindAsync<SalesOrder>(salesOrderId);

        salesOrder.Should().NotBeNull();
    }
}
