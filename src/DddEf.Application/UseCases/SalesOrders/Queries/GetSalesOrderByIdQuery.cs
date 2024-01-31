using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.ValueObjects;
using MediatR;
using static Dapper.SqlMapper;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class GetSalesOrderByIdQuery : IRequest<SalesOrderRes>
{ 
    public Guid Id { get; set; }
}
public class GetSalesOrderByIdQueryHandler(ISqlDapperClient dapperClient, IDddEfContext dddEfContext) : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderRes>
{
    public async Task<SalesOrderRes> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var ssql = """
            SELECT  T0.Id,
                    T0.TransNo,
                    T0.TransDate,
                    T0.Status,
                    T0.CustomerId,
                    T1.CustomerCode,
               	    T1.CustomerName,
                    T0.ShipAddress_City,
                    T0.ShipAddress_Country,
                    T0.BillAddress_City,
                    T0.BillAddress_Country,
                    T0.Total
            FROM Tx_SalesOrder T0
            LEFT JOIN Tm_Customer T1 ON T0.CustomerId=T1.Id
            WHERE T0.Id=@Id; 

            SELECT T0.RowNumber, T0.ItemId, T1.ItemCode, T1.ItemName, T0.Qty, T0.Price, T0.Total
            FROM Tx_SalesOrder_Item T0
            INNER JOIN Tm_Item T1 ON T0.ItemId=T1.Id
            WHERE T0.Id=@Id
            ORDER BY T0.RowNumber
            """;

        var result =
            await dapperClient.QueryMultipleAsync<SalesOrderRes>(
                                    ssql,
                                    iduConvert: async (GridReader salesOrderWithDetails) =>
                                    {
                                        using (salesOrderWithDetails)
                                        {
                                            var temp_order =  await salesOrderWithDetails.ReadFirstAsync<TempSalesOrderVm>();
                                            var order = new SalesOrderRes
                                            {
                                                Id = temp_order.Id,
                                                TransNo = temp_order.TransNo,
                                                Status = temp_order.Status,
                                                CustomerId = temp_order.CustomerId,
                                                CustomerCode = temp_order.CustomerCode,
                                                CustomerName = temp_order.CustomerName,
                                                ShipAddress = new Address(temp_order.ShipAddress_City, temp_order.ShipAddress_City),
                                                BillAddress = new Address(temp_order.BillAddress_City, temp_order.BillAddress_Country),
                                                Total = temp_order.Total,
                                                Items = (await salesOrderWithDetails.ReadAsync<SalesOrderItemVm>()).ToList()
                                            };

                                            return order;
                                        }
                                    },
                                    param: new { Id = request.Id }
                                );
        return result; 
    }
}

file class TempSalesOrderVm
{
    public Guid? Id { set; get; }
    public string TransNo { set; get; }
    public DateTime? TransDate { set; get; }
    public SalesOrderStatus Status { set; get; }
    public Guid? CustomerId { set; get; }
    public string CustomerCode { set; get; }
    public string CustomerName { set; get; }
    public string ShipAddress_City { set; get; }
    public string ShipAddress_Country { set; get; }
    public string BillAddress_City { set; get; }
    public string BillAddress_Country { set; get; }
    public double? Total { set; get; }
}