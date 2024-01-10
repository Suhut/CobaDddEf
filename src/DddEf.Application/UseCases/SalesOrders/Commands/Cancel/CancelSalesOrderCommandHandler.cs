using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Cancel;

public sealed class CancelSalesOrderCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<CancelSalesOrderCommand, Guid>
{
    public async Task<Guid> Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await dddEfContext.SalesOrders.FindAsync(new object[] { new SalesOrderId(request.Id) }, cancellationToken: cancellationToken);

        ArgumentNullException.ThrowIfNull(salesOrder);

        salesOrder.Cancel();
        //dddEfContext.SalesOrders.Update(salesOrder);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id.Value;
    }
}
