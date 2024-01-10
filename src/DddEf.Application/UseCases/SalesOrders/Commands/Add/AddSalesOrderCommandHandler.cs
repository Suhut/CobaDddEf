﻿using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using MediatR;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add
{
    public sealed class AddSalesOrderCommandHandler(IDddEfContext applicationDbContext) : IRequestHandler<AddSalesOrderCommand, Guid>
    {
        public async Task<Guid> Handle(AddSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var rowNumber = 1;
            var salesOrder = SalesOrder.Create(
             request.TransNo,
             request.TransDate,
             request.CustomerId,
             request.ShipAddress,
             request.BillAddress,
             request.Items.ConvertAll(item => SalesOrderItem.Create(
                 rowNumber++,
                 ItemId.Create(item.ItemId),
                 item.Qty,
                 item.Price
                 )));

            await applicationDbContext.SalesOrders.AddAsync(salesOrder, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return salesOrder.Id.Value;
        }
    }
}
