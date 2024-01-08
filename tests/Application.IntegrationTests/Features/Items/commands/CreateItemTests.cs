using DddEf.Application.UseCases.Items.Commands;
using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.Items.commands;

using static Testing;

public class CreateItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateItem()
    {
        // Arrange
        var command = new CreateItemCommand
        (
            "Code001",
            "Name001"
        );

        // Act
        var productId = await SendAsync(command);

        // Assert
        var product = await FindAsync<Item>(productId);

        product.Should().NotBeNull();
        product!.ItemCode.Should().Be(command.ItemCode);
        product.ItemName.Should().Be(command.ItemName);
    }
}
