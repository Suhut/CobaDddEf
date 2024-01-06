using DddEf.Application.Common.Interfaces;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Cancel
{
    public sealed class CancelSalesOrderCommandHandler(IDddEfContext applicationDbContext) : IRequestHandler<CancelSalesOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await applicationDbContext.SalesOrders.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

            ArgumentNullException.ThrowIfNull(salesOrder);

            salesOrder.Cancel();
            applicationDbContext.SalesOrders.Update(salesOrder);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return salesOrder.Id.Value;
        }
    }
}
