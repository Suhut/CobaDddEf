﻿using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Customers.commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{ 
    [Test]
    public async Task ShouldCreateCustomer()
    {
        // Arrange
        var command = new CreateCustomerCommand
        (
            "Code001",
            "Name001"
        );

        // Act
        var customerId = await SendAsync(command);

        // Assert
        var customer = await FindAsync<Customer>(CustomerId.Create(customerId));

        customer.Should().NotBeNull();
        customer!.CustomerCode.Should().Be(command.CustomerCode);
        customer.CustomerName.Should().Be(command.CustomerName); 

        
    }
}
