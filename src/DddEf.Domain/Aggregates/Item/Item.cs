﻿using DddEf.Domain.Aggregates.Item.ValueObjects;
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

    private Item(ItemId id, string productCode, string productName) 
    {
        Id = id;
        ItemCode = productCode;
        ItemName = productName;
    }

    public static Item Create(string productCode, string productName)
    {
        return new(new ItemId(Guid.NewGuid()), productCode, productName);
    }
}