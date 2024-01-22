using DddEf.Domain.Aggregates.Customer.ValueObjects;

namespace DddEf.Application.UseCases.Customers.Queries;

public class CustomerRes
{
    public CustomerId Id { set; get; }
    public string CustomerCode { set; get; } 
    public string CustomerName { set; get; } 
}
