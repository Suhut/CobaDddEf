using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Items.Commands;
public record CreateItemCommand
(
   string ItemCode,
   string ItemName
) : IRequest<ItemId>;
