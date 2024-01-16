namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class SalesOrderItemSecondVm
{
    public int? RowNumber { get; set; }
    public Guid? ItemId { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public double? Qty { get; set; }
    public double? Price { get; set; }
    public double? Total { get; set; }
    public List<SalesOrderItemSecondBinVm> Bins { set; get; } = [];
}
