using DddEf.Application.Common;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Close
{
    public sealed class CloseSalesOrderCommandHandler(IDddEfContext applicationDbContext) : IRequestHandler<CloseSalesOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CloseSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await applicationDbContext.SalesOrders.FindAsync(new object[] { request.Id }, cancellationToken);

            ArgumentNullException.ThrowIfNull(salesOrder);

            salesOrder.Close();
            applicationDbContext.SalesOrders.Update(salesOrder);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return salesOrder.Id.Value;
        }
    }
}
