using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;
public record AddSalesOrderCommand
(
    string TransNo,
    DateTime TransDate,
    CustomerId CustomerId,
    Address ShipAddress,
    Address BillAddress,
    List<AddSalesOrderItemVm> Items,
    List<AddSalesOrderItemSecondVm> ItemSeconds
) : IRequest<SalesOrderId>;
