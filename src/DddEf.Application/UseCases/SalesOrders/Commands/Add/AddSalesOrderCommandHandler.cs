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
        //var rowNumberItem = 1;
        //var rowNumberItemSecond = 1;

        var salesOrder = SalesOrder.Create(
         request.TransNo,
         request.TransDate,
         new CustomerId(request.CustomerId),
         request.ShipAddress,
         request.BillAddress,
         request.Items.Select((value, index) => SalesOrderItem.Create(
               index + 1,
             new ItemId(value.ItemId),
             value.Qty,
             value.Price,
             value.Bins.Select((value, index) => SalesOrderItemBin.Create(index + 1, value.BinName)).ToList()
             )).ToList(),
         request.ItemSeconds.Select((value, index) => SalesOrderItemSecond.Create(
             index + 1,
             new ItemId(value.ItemId),
             value.Qty,
             value.Price,
             value.Bins.Select((value, index) => SalesOrderItemSecondBin.Create(index + 1, value.BinName)).ToList()
             )).ToList()
         );

        await dddEfContext.SalesOrders.AddAsync(salesOrder, cancellationToken);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id.Value;
    }
}
