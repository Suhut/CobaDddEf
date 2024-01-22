using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DddEf.Application.UseCases.Customers.Queries;

public class GetCustomerByIdQuery : IRequest<CustomerRes>
{
    public CustomerId Id { get; set; }
}
public class GetCustomerByIdQueryHandler(IDddEfContext dddEfContext) : IRequestHandler<GetCustomerByIdQuery, CustomerRes>
{
    public async Task<CustomerRes> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {


        var result = await (
                    from T0 in dddEfContext.Customers.AsNoTracking()
                    where T0.Id.Equals(request.Id)
                    select new CustomerRes
                    {
                        Id = T0.Id,
                        CustomerCode = T0.CustomerCode,
                        CustomerName = T0.CustomerName,
                    }
            ).FirstOrDefaultAsync();

        return result;
    }
}
