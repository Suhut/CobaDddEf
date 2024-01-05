using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Infrastructure.Persistence;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Cancel
{
    public sealed class CancelSalesOrderCommandHandler : IRequestHandler<CancelSalesOrderCommand, Guid>
    {
        private readonly DddEfContext _dbContext;
        public CancelSalesOrderCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _dbContext.SalesOrders.FindAsync(request.Id);

            ArgumentNullException.ThrowIfNull(salesOrder);

            salesOrder.Cancel(); 
            _dbContext.SalesOrders.Update(salesOrder);
            await _dbContext.SaveChangesAsync();

            return salesOrder.Id.Value;
        }
    }
}
