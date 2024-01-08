using DddEf.Application.Common.Interfaces;
using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Items.Commands
{
    public sealed class AddItemCommandHandler(IDddEfContext applicationDbContext) : IRequestHandler<AddItemCommand, ItemId>
    { 
        public async Task<ItemId> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var product = Item.Create
            (
                 request.ItemCode,
                 request.ItemName
            );
            await applicationDbContext.Items.AddAsync(product, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
