using DddEf.Application.Common;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine
{
    public sealed class RemoveLineSalesOrderCommandHandler(IDddEfContext applicationDbContext) : IRequestHandler<RemoveLineSalesOrderCommand, Guid>
    {
        public async Task<Guid> Handle(RemoveLineSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await applicationDbContext.SalesOrders.FindAsync(new object[] { request.Id }, cancellationToken);

            ArgumentNullException.ThrowIfNull(salesOrder);

            salesOrder.RemoveLine();

            applicationDbContext.SalesOrders.Update(salesOrder);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return salesOrder.Id.Value;
        }
    }
}
