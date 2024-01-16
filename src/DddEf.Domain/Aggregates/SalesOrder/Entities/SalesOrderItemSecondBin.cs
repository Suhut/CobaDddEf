namespace DddEf.Domain.Aggregates.SalesOrder.Entities;

public sealed class SalesOrderItemSecondBin
{
    private Guid DetDetId { get; set; }
    private Guid DetId { get; set; } 
    public int RowNumber { get; private set; }
    public string BinName { get; private set; }


#pragma warning disable CS8618
    private SalesOrderItemSecondBin()
    {

    }
#pragma warning disable CS8618 

    public SalesOrderItemSecondBin(
                        int rowNumber,
                       string binName
       )
    {
        DetId = Guid.NewGuid();
        RowNumber = rowNumber;
        BinName = binName;
    }
    public static SalesOrderItemSecondBin Create(
                        int rowNumber,
                       string binName)
    {
        return new(rowNumber, binName);
    }

}

