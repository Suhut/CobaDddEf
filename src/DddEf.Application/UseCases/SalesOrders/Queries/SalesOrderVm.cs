using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.ValueObjects;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class SalesOrderVm
{
    public Guid? Id { set; get; }
    public string TransNo { set; get; }
    public DateTime? TransDate { set; get; }
    public SalesOrderStatus Status { set; get; }
    public Guid? CustomerId { set; get; }
    public string CustomerCode { set; get; }
    public string CustomerName { set; get; }
    public decimal? TotalTc { set; get; }
    public Address ShipAddress { set; get; } 
    public Address BillAddress { set; get; } 
}
