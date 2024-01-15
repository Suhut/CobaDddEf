namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class SalesOrderItemVm
{
    public int? RowNumber { get; set; }
    public Guid? ItemId { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public decimal? Qty { get; set; }
    public decimal? Price { get; set; }
    public decimal? Total { get; set; }
}
