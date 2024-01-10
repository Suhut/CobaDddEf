using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;

namespace DddEf.Domain.Aggregates.SalesOrder.Entities;

public sealed class SalesOrderItem  
{
    private Guid DetId { get; set; }
    private SalesOrderId Id { get; }
    public int RowNumber { get; private set; }
    public ItemId ItemId { get; private set; }
    public double? Qty { get; private set; }
    public double? Price { get; private set; }
    public double? Total { get; private set; }
    public string LineStatus { get; private set; }


#pragma warning disable CS8618
    private SalesOrderItem()
    {
       
    }
#pragma warning disable CS8618 

    public SalesOrderItem( 
                        int rowNumber,
                        ItemId itemId,
                       double qty,
                       double price
       ) 
    {
        DetId = Guid.NewGuid();
        RowNumber = rowNumber;
        ItemId = itemId;
        Qty = qty;
        Price = price;
        Total = qty * price;
        LineStatus = "Open";
    }
    public static SalesOrderItem Create(
                        int rowNumber,
                        ItemId itemId,
                       double qty,
                       double price)
    { 
        return new( rowNumber, itemId, qty, price);
    }

    public void Close()
    {
        LineStatus = "Closed";
    }
}
 
