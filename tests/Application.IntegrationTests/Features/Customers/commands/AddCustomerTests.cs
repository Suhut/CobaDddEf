using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.Customers.commands;

using static Testing;

public class AddItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldAddCustomer()
    {
        // Arrange
        var command = new AddCustomerCommand
        (
            "Code001",
            "Name001"
        );

        // Act
        var customerId = await SendAsync(command);

        // Assert
        var customer = await FindAsync<Customer>(customerId);

        customer.Should().NotBeNull();
        customer!.CustomerCode.Should().Be(command.CustomerCode);
        customer.CustomerName.Should().Be(command.CustomerName);


    }
}
