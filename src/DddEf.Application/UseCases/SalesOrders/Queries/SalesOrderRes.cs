using DddEf.Domain.Common.ValueObjects;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class SalesOrderRes
{
    public Guid? Id { set; get; }
    public string TransNo { set; get; }
    public DateTime? TransDate { set; get; }
    public string Status { set; get; }
    public Guid? CustomerId { set; get; }
    public string CustomerCode { set; get; }
    public string CustomerName { set; get; }
    public double? Total { set; get; }
    public Address ShipAddress { set; get; } 
    public Address BillAddress { set; get; }
    public List<SalesOrderItemVm> Items { set; get; } = [];
}
