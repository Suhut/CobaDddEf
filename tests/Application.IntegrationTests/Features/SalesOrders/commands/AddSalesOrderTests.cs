using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Items.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Application.UseCases.SalesOrders.Queries;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
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

        var addItemCommand3 = new AddItemCommand
        (
            "ItemCode003",
            "ItemName003"
        );
        var itemId3 = await SendAsync(addItemCommand3);

        var addItemCommand4 = new AddItemCommand
        (
            "ItemCode004",
            "ItemName004"
        );
        var itemId4 = await SendAsync(addItemCommand4);

        var addItemCommand5 = new AddItemCommand
        (
            "ItemCode005",
            "ItemName005"
        );
        var itemId5 = await SendAsync(addItemCommand5);


        var createSalesOrderCommand01 = new AddSalesOrderCommand
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
            new List<AddSalesOrderItemSecondVm>
            {
                new AddSalesOrderItemSecondVm(itemId3,3,3000),
                new AddSalesOrderItemSecondVm(itemId4,4,4000),
                new AddSalesOrderItemSecondVm(itemId5,5,5000)
            }
        );

        var createSalesOrderCommand02 = new AddSalesOrderCommand
                (
                    "Trans001",
                    DateTime.Now.Date,
                    customerId,
                    new Address("Blora", "Indonesia"),
                    new Address("Jakarta", "Indonesia"),
                    new List<AddSalesOrderItemVm>
                    {
                new AddSalesOrderItemVm(itemId1,5,5000),
                new AddSalesOrderItemVm(itemId2,4,4000)
                    },
                    new List<AddSalesOrderItemSecondVm>
                    {
                new AddSalesOrderItemSecondVm(itemId3,3,3000),
                new AddSalesOrderItemSecondVm(itemId4,2,2000),
                new AddSalesOrderItemSecondVm(itemId5,1,1000)
                    }
                );


        // Act
        var salesOrderId01 = await SendAsync(createSalesOrderCommand01);
        var salesOrderId02 = await SendAsync(createSalesOrderCommand02);


        //// Assert
        //{

        //    var salesOrder = await FindAsync<SalesOrder>(new SalesOrderId(salesOrderId01));

        //    salesOrder.Should().NotBeNull();
        //    //salesOrder!.TransNo.Should().Be(createSalesOrderCommand.TransNo);
        //    //salesOrder.TransDate.Should().Be(createSalesOrderCommand.TransDate);
        //    //salesOrder.Status.Should().Be("Open");
        //    //salesOrder.ShipAddress.Country.Should().Be(createSalesOrderCommand.ShipAddress.Country);
        //    //salesOrder.ShipAddress.City.Should().Be(createSalesOrderCommand.ShipAddress.City);
        //    //salesOrder.BillAddress.Country.Should().Be(createSalesOrderCommand.BillAddress.Country);
        //    //salesOrder.BillAddress.City.Should().Be(createSalesOrderCommand.BillAddress.City);
        //    //salesOrder.Items.Should().NotBeNull();
        //    //salesOrder.Items.Count.Should().Be(createSalesOrderCommand.Items.Count);

        //    var getSalesOrderByIdQuery = new GetSalesOrderByIdQuery { Id = salesOrderId01 };
        //    var salesOrder1 = await SendAsync(getSalesOrderByIdQuery);

        //    salesOrder1.Should().NotBeNull();
        //}

        //{

        //    var getSalesOrdersQuery = new GetSalesOrdersQuery();
        //    var salesOrders = await SendAsync(getSalesOrdersQuery);
        //    salesOrders.Should().NotBeNull();

        //}

        {

            var getSalesOrdersQuery = new GetSalesOrdersLinqQuery { DateFrom= DateTime.Now.Date, DateTo = DateTime.Now.Date };
            var salesOrders = await SendAsync(getSalesOrdersQuery);
            salesOrders.Should().NotBeNull();

        }

    }
}
