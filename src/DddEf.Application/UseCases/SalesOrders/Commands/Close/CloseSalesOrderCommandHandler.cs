using DddEf.Infrastructure.Persistence;
using MediatR;
using System.Threading;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Close
{
    public sealed class CloseSalesOrderCommandHandler : IRequestHandler<CloseSalesOrderCommand, Guid>
    {
        private readonly DddEfContext _dbContext;
        public CloseSalesOrderCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CloseSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _dbContext.SalesOrders.FindAsync(request.Id);

            ArgumentNullException.ThrowIfNull(salesOrder);

            salesOrder.Close(); 
            _dbContext.SalesOrders.Update(salesOrder);
            await _dbContext.SaveChangesAsync();

            return salesOrder.Id.Value;
        }
    }
}
