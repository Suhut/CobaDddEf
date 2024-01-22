using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add;

public sealed class AddSalesOrderCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<AddSalesOrderCommand, SalesOrderId>
{
    public async Task<SalesOrderId> Handle(AddSalesOrderCommand request, CancellationToken cancellationToken)
    { 
        var salesOrder = SalesOrder.Create(
         request.TransNo,
         request.TransDate,
         request.CustomerId,
         request.ShipAddress,
         request.BillAddress,
         request.Items.Select((item, itemIndex) => SalesOrderItem.Create(
               itemIndex + 1,
             item.ItemId,
             item.Qty,
             item.Price,
             item.Bins.Select((bin, binIndex) => SalesOrderItemBin.Create(binIndex + 1, bin.BinName)).ToList()
             )).ToList(),
         request.ItemSeconds.Select((item, itemIndex) => SalesOrderItemSecond.Create(
             itemIndex + 1,
              item.ItemId,
             item.Qty,
             item.Price,
             item.Bins.Select((bin, binIndex) => SalesOrderItemSecondBin.Create(binIndex + 1, bin.BinName)).ToList()
             )).ToList()
         );

        await dddEfContext.SalesOrders.AddAsync(salesOrder, cancellationToken);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return salesOrder.Id;
    }
}
