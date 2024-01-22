using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine;

public sealed class RemoveLineSalesOrderCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<RemoveLineSalesOrderCommand, SalesOrderId>
{
    public async Task<SalesOrderId> Handle(RemoveLineSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await dddEfContext.SalesOrders.FindAsync(new object[] { request.Id }, cancellationToken);

        ArgumentNullException.ThrowIfNull(salesOrder);

        salesOrder.RemoveLine();

        //dddEfContext.SalesOrders.Update(salesOrder);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id;
    }
}
