using DddEf.Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Customers.Commands;

public record CreateCustomerCommand
(
    string CustomerCode,
    string CustomerName
) : IRequest<CustomerId>;