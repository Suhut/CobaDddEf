using DddEf.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class GetSalesOrdersLinq02Query : IRequest<List<SalesOrderVm>>
{
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}
public class GetSalesOrdersLinq02QueryHandler(IDddEfContext dddEfContext) : IRequestHandler<GetSalesOrdersLinq02Query, List<SalesOrderVm>>
{
    public async Task<List<SalesOrderVm>> Handle(GetSalesOrdersLinq02Query request, CancellationToken cancellationToken)
    {
        var salesOrders = (from T0 in dddEfContext.SalesOrders
                           join T3 in dddEfContext.Customers.AsNoTracking() on T0.CustomerId equals T3.Id
                           where T0.TransDate >= request.DateFrom && T0.TransDate <= request.DateTo
                           select new SalesOrderVm
                           {
                               Id = T0.Id.Value,
                               TransNo = T0.TransNo,
                               TransDate = T0.TransDate,
                               Status = T0.Status,
                               CustomerId = T0.CustomerId.Value,
                               CustomerCode = T3.CustomerCode,
                               CustomerName = T3.CustomerName, 
                               ShipAddress = T0.ShipAddress,
                               BillAddress = T0.BillAddress
                           }
                     ).Skip(1).Take(10).ToList();


        return salesOrders;
    }
}

