using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DddEf.Application.UseCases.Customers.Commands
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly DddEfContext _dbContext;
        public CreateProductCommandHandler(DddEfContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create
            (
                 request.CustomerCode,
                 request.CustomerName
            );
             await _dbContext.Customers.AddAsync(customer);
             
            await _dbContext.SaveChangesAsync();

            CustomerId[] arrs= {   CustomerId.Create(customer.Id.Value), CustomerId.CreateUnique() };

            var customers= await _dbContext.Customers.Where(x=>x.CustomerCode == request.CustomerCode && !arrs.Contains(x.Id)).ToListAsync();
            return customer.Id.Value;
        }
    }
}
