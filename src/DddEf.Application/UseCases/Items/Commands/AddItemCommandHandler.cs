using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Items.Commands;

public sealed class AddItemCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<AddItemCommand, ItemId>
{ 
    public async Task<ItemId> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = Item.Create
        (
             request.ItemCode,
             request.ItemName
        );
        await dddEfContext.Items.AddAsync(item, cancellationToken);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
