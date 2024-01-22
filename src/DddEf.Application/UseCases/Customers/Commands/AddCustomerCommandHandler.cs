using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Customer;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Customers.Commands;

public sealed class AddItemCommandHandler (IDddEfContext dddEfContext) : IRequestHandler<AddCustomerCommand, CustomerId>
{ 
    public async Task<CustomerId> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create
        (
             request.CustomerCode,
             request.CustomerName
        );
        await dddEfContext.Customers.AddAsync(customer, cancellationToken);

        await dddEfContext.SaveChangesAsync(cancellationToken);
          
        return customer.Id;
    }
}
