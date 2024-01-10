﻿using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;
public record AddSalesOrderCommand
(
    string TransNo,
    DateTime TransDate,
    Guid CustomerId,
    Address ShipAddress,
    Address BillAddress,
    List<SalesOrderItemVm> Items
) : IRequest<Guid>;

public record SalesOrderItemVm(
    Guid ItemId,
    double Qty,
    double Price
);
