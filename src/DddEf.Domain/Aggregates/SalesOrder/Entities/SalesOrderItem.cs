using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;

namespace DddEf.Domain.Aggregates.SalesOrder.Entities;

public sealed class SalesOrderItem  
{
#pragma warning disable CS8618
    private SalesOrderItem()
    {
       
    }
#pragma warning disable CS8618 
    private Guid DetId { get; set; }
    public SalesOrderId Id { get; }
    public int RowNumber { get; private set; }
    public ProductId ProductId { get; private set; }
    public double? Qty { get; private set; }
    public double? Price { get; private set; }
    public double? Total { get; private set; }
    public SalesOrderItem( 
                        int rowNumber,
                        ProductId productId,
                       double qty,
                       double price
       ) 
    {
        DetId = Guid.NewGuid();
        RowNumber = rowNumber;
        ProductId = productId;
        Qty = qty;
        Price = price;
        Total = qty * price;
    }
    public static SalesOrderItem Create(
                        int rowNumber,
                        ProductId productId,
                       double qty,
                       double price)
    { 
        return new( rowNumber, productId, qty, price);
    }
}
 
