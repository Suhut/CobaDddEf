using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.Item.ValueObjects;

public sealed class ItemId : ValueObject
{
    public Guid Value { get; }
#pragma warning disable CS8618
    private ItemId()
    {
    }
#pragma warning disable CS8618

    private ItemId(Guid value) => Value = value;

    public static ItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static ItemId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static implicit operator Guid(ItemId id)
    {
        return id.Value;
    }
}