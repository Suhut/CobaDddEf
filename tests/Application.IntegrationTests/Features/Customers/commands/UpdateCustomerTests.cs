using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.Customers.commands;

using static Testing;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateCustomer()
    {
        // Arrange
        var command = new AddCustomerCommand
        (
            "Code001",
            "Name001"
        );
        var customerId = await SendAsync(command);  

        // Act
        var commandUpdate = new UpdateCustomerCommand
       (
            customerId,
           "Code001",
           "Name001"
       );
        var customerId2 = await SendAsync(commandUpdate);

        // Assert

        var customer2 = await FindAsync<Customer>(customerId2);
        customer2.Should().NotBeNull();


    }
}
