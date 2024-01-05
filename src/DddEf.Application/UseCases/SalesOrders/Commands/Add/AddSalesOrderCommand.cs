﻿using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;
public record AddSalesOrderCommand
(
    string TransNo,
    DateTime TransDate,
    CustomerId CustomerId,
    Address ShipAddress,
    Address BillAddress,
    List<SalesOrderItemVm> Items
) : IRequest<SalesOrderId>;

public record SalesOrderItemVm(
    ProductId ProductId,
    double Qty,
    double Price
);