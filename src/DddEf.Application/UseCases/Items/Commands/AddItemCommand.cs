using MediatR;

namespace DddEf.Application.UseCases.Items.Commands;
public record AddItemCommand
(
   string ItemCode,
   string ItemName
) : IRequest<Guid>;
