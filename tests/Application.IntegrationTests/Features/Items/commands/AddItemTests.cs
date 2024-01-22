using DddEf.Application.UseCases.Items.Commands;
using DddEf.Domain.Aggregates.Item;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Features.Items.commands;

using static Testing;

public class AddItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldAddItem()
    {
        // Arrange
        var command = new AddItemCommand
        (
            "Code001",
            "Name001"
        );

        // Act
        var itemId = await SendAsync(command);

        // Assert
        var item = await FindAsync<Item>(itemId);

        item.Should().NotBeNull();
        item!.ItemCode.Should().Be(command.ItemCode);
        item.ItemName.Should().Be(command.ItemName);
    }
}
