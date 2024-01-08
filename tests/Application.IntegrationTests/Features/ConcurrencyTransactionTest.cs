using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Items.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands.Add;
using DddEf.Domain.Common.ValueObjects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using DddEf.Domain.Aggregates.SalesOrder;

namespace DddEf.Application.IntegrationTests.Features.SalesOrders;

using static Testing;

public class ConcurrencyTransactionTest : BaseTestFixture
{
    [Test]
    public async Task ShouldError()
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



        var salesOrderId = await SendAsync(createSalesOrderCommand);


        var entity1 = await FindAsync<SalesOrder>(salesOrderId);
        var entity2 = await FindAsync<SalesOrder>(salesOrderId);


        entity1.Close();
        await UpdateAsync(entity1);

        // Act  
        entity2.Cancel();

        // Assert
        FluentActions.Invoking(() =>
               UpdateAsync(entity2)).Should().ThrowAsync<DbUpdateConcurrencyException>();
    }
}
