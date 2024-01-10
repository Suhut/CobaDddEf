using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.Item;

public sealed class Item : AggregateRoot
{
    public ItemId Id { get; private set; }
    public string ItemCode { get; private set; }
    public string ItemName { get; private set; }

#pragma warning disable CS8618
    private Item()
    {

    }

#pragma warning disable CS8618

    private Item(ItemId id, string itemCode, string itemName) 
    {
        Id = id;
        ItemCode = itemCode;
        ItemName = itemName;
    }

    public static Item Create(string itemCode, string itemName)
    {
        return new(new ItemId(Guid.NewGuid()), itemCode, itemName);
    }
}