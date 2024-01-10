using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Item;
using MediatR;

namespace DddEf.Application.UseCases.Items.Commands;

public sealed class AddItemCommandHandler(IDddEfContext dddEfContext) : IRequestHandler<AddItemCommand, Guid>
{ 
    public async Task<Guid> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = Item.Create
        (
             request.ItemCode,
             request.ItemName
        );
        await dddEfContext.Items.AddAsync(item, cancellationToken);
        await dddEfContext.SaveChangesAsync(cancellationToken);

        return item.Id.Value;
    }
}
