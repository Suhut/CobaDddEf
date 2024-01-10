using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine;
public record RemoveLineSalesOrderCommand
( 
    Guid Id
) : IRequest<Guid>;
 