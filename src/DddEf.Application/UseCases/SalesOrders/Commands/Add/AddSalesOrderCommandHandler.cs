using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;

public sealed class AddSalesOrderCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<AddSalesOrderCommand, Guid>
{
    public async Task<Guid> Handle(AddSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var rowNumber = 1;
        var salesOrder = SalesOrder.Create(
         request.TransNo,
         request.TransDate,
         new CustomerId(request.CustomerId),
         request.ShipAddress,
         request.BillAddress,
         request.Items.ConvertAll(item => SalesOrderItem.Create(
             rowNumber++,
             new ItemId(item.ItemId),
             item.Qty,
             item.Price
             )));

        await dddEfContext.SalesOrders.AddAsync(salesOrder, cancellationToken);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id.Value;
    }
}
