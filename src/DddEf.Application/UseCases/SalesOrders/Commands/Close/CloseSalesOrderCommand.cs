using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Close;
public record CloseSalesOrderCommand
( 
    Guid Id
) : IRequest<Guid>;
 