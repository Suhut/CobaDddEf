using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine;

public sealed class RemoveLineSalesOrderCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<RemoveLineSalesOrderCommand, Guid>
{
    public async Task<Guid> Handle(RemoveLineSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await dddEfContext.SalesOrders.FindAsync(new object[] { new SalesOrderId(request.Id) }, cancellationToken);

        ArgumentNullException.ThrowIfNull(salesOrder);

        salesOrder.RemoveLine();

        //dddEfContext.SalesOrders.Update(salesOrder);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id.Value;
    }
}
