﻿using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Common.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace DddEf.Application.UseCases.SalesOrders.Queries;

public class GetSalesOrdersLinqQuery : IRequest<List<SalesOrderRes>>
{
    public Guid Id { get; set; }
}
public class GetSalesOrdersLinqQueryHandler(ISqlDapperClient dapperClient, IDddEfContext dddEfContext) : IRequestHandler<GetSalesOrdersLinqQuery, List<SalesOrderRes>>
{
    public async Task<List<SalesOrderRes>> Handle(GetSalesOrdersLinqQuery request, CancellationToken cancellationToken)
    {
        var test= (from T0 in dddEfContext.SalesOrders 
                   //from T1 in T0.Items
                   //join T2 in dddEfContext.Items on T1.ItemId equals T2.Id
                   join T3 in dddEfContext.Customers on T0.CustomerId equals T3.Id
                     select new SalesOrderRes
                     {
                         Id= T0.Id.Value, 
                         TransNo = T0.TransNo,
                         TransDate = T0.TransDate,
                         Status = T0.Status,
                         CustomerId = T0.CustomerId.Value,
                         CustomerCode = T3.CustomerCode,
                         CustomerName = T3.CustomerName,
                         Total = T0.Total,
                         ShipAddress = T0.ShipAddress,
                         BillAddress = T0.BillAddress,
                         Items =(from T0_ in T0.Items
                                 join T1_ in dddEfContext.Items.AsNoTracking() on T0_.ItemId equals T1_.Id
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

                         ).AsList()
                     }
                     ).AsList();


        return test;
    }
}

