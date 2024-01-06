using DddEf.Application.Common;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DddEf.Application.UseCases.Customers.Commands
{
    public sealed class CreateProductCommandHandler (IDddEfContext applicationDbContext) : IRequestHandler<CreateCustomerCommand, CustomerId>
    { 
        public async Task<CustomerId> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create
            (
                 request.CustomerCode,
                 request.CustomerName
            );
            await applicationDbContext.Customers.AddAsync(customer, cancellationToken);

            await applicationDbContext.SaveChangesAsync(cancellationToken);

            CustomerId[] arrs = { CustomerId.Create(customer.Id.Value), CustomerId.CreateUnique() };

            var customers = await applicationDbContext.Customers.Where(x => x.CustomerCode == request.CustomerCode && !arrs.Contains(x.Id)).ToListAsync(cancellationToken);
            return customer.Id;
        }
    }
}
