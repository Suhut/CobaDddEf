using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Products.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Common.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.SalesOrder.commands;

using static Testing;

public class AddSalesOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateCustomer()
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


        // Act
        var salesOrderId = await SendAsync(createSalesOrderCommand);


        // Assert
        var salesOrder = await FindAsync<DddEf.Domain.Aggregates.SalesOrder.SalesOrder>(salesOrderId);

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
