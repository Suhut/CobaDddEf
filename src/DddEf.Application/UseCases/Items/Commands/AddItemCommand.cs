using DddEf.Domain.Aggregates.Item;
using DddEf.Domain.Aggregates.Item.ValueObjects;
using MediatR;

namespace DddEf.Application.UseCases.Items.Commands;
public record AddItemCommand
(
   string ItemCode,
   string ItemName
) : IRequest<ItemId>;
