using FluentValidation;

namespace DddEf.Application.UseCases.SalesOrders.Commands.RemoveLine
{
    public class RemoveLineSalesOrderCommandValidator : AbstractValidator<RemoveLineSalesOrderCommand>
    {
        public RemoveLineSalesOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty(); 
        }
    }
}
