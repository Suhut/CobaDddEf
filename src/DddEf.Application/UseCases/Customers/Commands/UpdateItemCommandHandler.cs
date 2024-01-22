using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Customers.Commands;

public sealed class UpdateItemCommandHandler (IDddEfContext dddEfContext) : IRequestHandler<UpdateCustomerCommand, CustomerId>
{ 
    public async Task<CustomerId> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    { 
        var customer= await dddEfContext.Customers.FindAsync(new object[] {  request.Id }, cancellationToken: cancellationToken ); 

        ArgumentNullException.ThrowIfNull(customer);

        customer.Edit(request.CustomerCode, request.CustomerName);

        await dddEfContext.SaveChangesAsync(cancellationToken);
          
        return customer.Id;
    }
}
