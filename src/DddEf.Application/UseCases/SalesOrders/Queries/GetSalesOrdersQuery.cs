using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Common.ValueObjects;
using MediatR;
using static Dapper.SqlMapper;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class GetSalesOrdersQuery : IRequest<List<SalesOrderVm>>
{
    public Guid Id { get; set; }
}
public class GetSalesOrdersQueryHandler(ISqlDapperClient dapperClient, IDddEfContext dddEfContext) : IRequestHandler<GetSalesOrdersQuery, List<SalesOrderVm>>
{
    public async Task<List<SalesOrderVm>> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
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
            """;

        var result =
            await dapperClient.QueryMultipleAsync<List<SalesOrderVm>>(
                                    ssql,
                                    iduConvert: async (GridReader salesOrderWithDetails) =>
                                    {
                                        using (salesOrderWithDetails)
                                        {
                                            var temp_orders = await salesOrderWithDetails.ReadAsync<TempSalesOrderVm>(); 

                                            var orders = (from T0 in temp_orders 
                                                          select new SalesOrderVm
                                                          {
                                                              Id = T0.Id,
                                                              TransNo = T0.TransNo,
                                                              Status = T0.Status,
                                                              CustomerId = T0.CustomerId,
                                                              CustomerCode = T0.CustomerCode,
                                                              CustomerName = T0.CustomerName,
                                                              ShipAddress = new Address(T0.ShipAddress_City, T0.ShipAddress_City),
                                                              BillAddress = new Address(T0.BillAddress_City, T0.BillAddress_Country),
                                                              TotalTc = T0.TotalTc,
                                                          }
                                                         );

                                            return orders.AsList();
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
    public string Status { set; get; }
    public Guid? CustomerId { set; get; }
    public string CustomerCode { set; get; }
    public string CustomerName { set; get; }
    public string ShipAddress_City { set; get; }
    public string ShipAddress_Country { set; get; }
    public string BillAddress_City { set; get; }
    public string BillAddress_Country { set; get; }
    public decimal? TotalTc { set; get; }
}
 