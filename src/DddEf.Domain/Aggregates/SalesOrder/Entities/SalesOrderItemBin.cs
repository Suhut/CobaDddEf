namespace DddEf.Domain.Aggregates.SalesOrder.Entities;

public sealed class SalesOrderItemBin
{
    private Guid DetDetId { get; set; }
    private Guid DetId { get; set; } 
    public int RowNumber { get; private set; }
    public string BinName { get; private set; }


#pragma warning disable CS8618
    private SalesOrderItemBin()
    {
       
    }
#pragma warning disable CS8618 

    public SalesOrderItemBin( 
                        int rowNumber, 
                       string binName
       ) 
    {
        DetId = Guid.NewGuid();
        RowNumber = rowNumber;
        BinName = binName;
    }
    public static SalesOrderItemBin Create(
                        int rowNumber, 
                       string binName)
    { 
        return new(rowNumber, binName);
    }
     
}
 
