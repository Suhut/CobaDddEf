using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Close;

public sealed class CloseSalesOrderCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<CloseSalesOrderCommand, Guid>
{
    public async Task<Guid> Handle(CloseSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var salesOrder = await dddEfContext.SalesOrders.FindAsync(new object[] { new SalesOrderId(request.Id) }, cancellationToken);

        ArgumentNullException.ThrowIfNull(salesOrder);

        salesOrder.Close();
        //dddEfContext.SalesOrders.Update(salesOrder);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id.Value;
    }
}
