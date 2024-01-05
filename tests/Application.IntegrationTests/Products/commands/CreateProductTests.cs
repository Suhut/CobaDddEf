using DddEf.Application.UseCases.Products.Commands;
using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Products.commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{ 
    [Test]
    public async Task ShouldCreateProduct()
    { 
        // Arrange
        var command = new CreateProductCommand
        (
            "Code001",
            "Name001"
        );

        // Act
        var productId = await SendAsync(command);

        // Assert
        var product = await FindAsync<Product>(productId);

        product.Should().NotBeNull();
        product!.ProductCode.Should().Be(command.ProductCode);
        product.ProductName.Should().Be(command.ProductName); 
    }
}
