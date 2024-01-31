namespace DddEf.Domain.Aggregates.SalesOrder.ValueObjects;

public enum SalesOrderStatus
{
    Draft = 0,
    Open = 1,
    Closed = 2,
    Cancelled = 3,
}