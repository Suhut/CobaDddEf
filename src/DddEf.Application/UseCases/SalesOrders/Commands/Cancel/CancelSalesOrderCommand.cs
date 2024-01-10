﻿using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Cancel;
public record CancelSalesOrderCommand
( 
    Guid Id
) : IRequest<Guid>;
 