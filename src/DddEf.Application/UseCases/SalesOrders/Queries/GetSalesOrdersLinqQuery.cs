using DddEf.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class GetSalesOrdersLinqQuery : IRequest<List<SalesOrderRes>>
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
public class GetSalesOrdersLinqQueryHandler(ISqlDapperClient dapperClient, IDddEfContext dddEfContext) : IRequestHandler<GetSalesOrdersLinqQuery, List<SalesOrderRes>>
{
    public async Task<List<SalesOrderRes>> Handle(GetSalesOrdersLinqQuery request, CancellationToken cancellationToken)
    { 
        var salesOrders = (from T0 in dddEfContext.SalesOrders 
                    join T3 in dddEfContext.Customers.AsNoTracking() on T0.CustomerId equals T3.Id
                    where T0.TransDate >= request.DateFrom && T0.TransDate <= request.DateTo
                    select new SalesOrderRes
                    {
                        Id = T0.Id.Value,
                        TransNo = T0.TransNo,
                        TransDate = T0.TransDate,
                        Status = T0.Status,
                        CustomerId = T0.CustomerId.Value,
                        CustomerCode = T3.CustomerCode,
                        CustomerName = T3.CustomerName,
                        Total = T0.Total,
                        ShipAddress = T0.ShipAddress,
                        BillAddress = T0.BillAddress,
                        Items = (
                                 from T0_ in T0.Items
                                 join T1_ in dddEfContext.Items.AsNoTracking() on T0_.ItemId equals T1_.Id
                                 where T0.TransDate >= request.DateFrom && T0.TransDate <= request.DateTo
                                 orderby T0_.RowNumber  
                                 select new SalesOrderItemVm
                                 {
                                     RowNumber = T0_.RowNumber,
                                     ItemId = T0_.ItemId.Value,
                                     ItemCode = T1_.ItemCode,
                                     ItemName = T1_.ItemName,
                                     Qty = T0_.Qty,
                                     Price = T0_.Price,
                                     Total = T0_.Total,
                                 }

                          ).AsList(),
                        ItemSeconds = (
                                 from T0_ in T0.ItemSeconds
                                 join T1_ in dddEfContext.Items.AsNoTracking() on T0_.ItemId equals T1_.Id
                                 where T0.TransDate >= request.DateFrom && T0.TransDate <= request.DateTo
                                 orderby T0_.RowNumber
                                 select new SalesOrderItemSecondVm
                                 {
                                     RowNumber = T0_.RowNumber,
                                     ItemId = T0_.ItemId.Value,
                                     ItemCode = T1_.ItemCode,
                                     ItemName = T1_.ItemName,
                                     Qty = T0_.Qty,
                                     Price = T0_.Price,
                                     Total = T0_.Total,
                                 }

                          ).AsList()
                    }
                     ).AsList();


        return salesOrders;
    }
}

