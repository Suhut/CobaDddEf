using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.Customer;

public sealed class Customer : AggregateRoot
{
    public CustomerId Id { get; private set; }
    public string CustomerCode { get; private set; }
    public string CustomerName { get; private set; }

#pragma warning disable CS8618
    private Customer()
    {

    }
#pragma warning disable CS8618 

    private Customer(CustomerId id, string customerCode, string customerName)
    {
        Id = id;
        CustomerCode = customerCode;
        CustomerName = customerName;
    }
    public static Customer Create(string customerCode, string customerName)
    {
        return new(new CustomerId(Guid.NewGuid()), customerCode, customerName);
    }



}