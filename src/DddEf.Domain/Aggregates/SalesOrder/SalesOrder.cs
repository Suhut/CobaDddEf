using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.Models;
using DddEf.Domain.Common.ValueObjects;

namespace DddEf.Domain.Aggregates.SalesOrder;
public sealed class SalesOrder : AggregateRoot 
{
    public SalesOrderId Id { get; private set; }
    public string TransNo { get; private set; }
    public DateTime TransDate { get; private set; }
    public string Status { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public Address ShipAddress { get; private set; }
    public Address BillAddress { get; private set; }
    public IReadOnlyList<SalesOrderItem> Items => (_items.OrderBy(p => p.RowNumber)).ToList().AsReadOnly();

    private readonly List<SalesOrderItem> _items = new();
    public double Total { get; private set; }


#pragma warning disable CS8618
    private SalesOrder()
    {

    }  
#pragma warning disable CS8618
   

    private SalesOrder(SalesOrderId id, string transNo, DateTime transDate, CustomerId customerId,
        Address billAddress,
        Address shipAddress,
        List<SalesOrderItem> items)
         
    { 
        Id = id;
        TransNo = transNo;
        TransDate = transDate;
        Status = "Open";
        CustomerId = customerId;
        BillAddress = billAddress;
        ShipAddress = shipAddress;
        _items = items;
        Total = items.Sum(p => p.Total ?? 0);
    }


    public static SalesOrder Create(string transNo, DateTime transDate, CustomerId customerId,
                                Address billAddress,
                                Address shipAddress,
                              List<SalesOrderItem> items)
    {
        return new(new SalesOrderId(Guid.NewGuid()), transNo, transDate, customerId, billAddress, shipAddress, items);
    }

    public void Cancel()
    { 
        Status = "Cancelled"; 
    }

    public void Close()
    {
        Status = "Closed";
        foreach(SalesOrderItem item in _items)
        {
            item.Close();
        }
    }

    public void RemoveLine()
    {
        var item= _items.OrderBy(p=>p.RowNumber).FirstOrDefault();
        _items.Remove(item);  
    }

}