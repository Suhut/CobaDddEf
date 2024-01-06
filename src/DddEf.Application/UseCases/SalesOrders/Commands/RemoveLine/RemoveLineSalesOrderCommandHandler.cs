using DddEf.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine
{
    public sealed class RemoveLineSalesOrderCommandHandler : IRequestHandler<RemoveLineSalesOrderCommand, Guid>
    {
        private readonly DddEfContext _dbContext;
        public RemoveLineSalesOrderCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(RemoveLineSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _dbContext.SalesOrders.FindAsync(request.Id);

            ArgumentNullException.ThrowIfNull(salesOrder);

            salesOrder.RemoveLine();

            _dbContext.SalesOrders.Update(salesOrder);
            await _dbContext.SaveChangesAsync();

            return salesOrder.Id.Value;
        }
    }
}
