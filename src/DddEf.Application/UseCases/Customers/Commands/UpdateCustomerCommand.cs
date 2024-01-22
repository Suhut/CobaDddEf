using DddEf.Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Customers.Commands;

public record UpdateCustomerCommand
(
    CustomerId Id,
    string CustomerCode,
    string CustomerName
) : IRequest<CustomerId>;