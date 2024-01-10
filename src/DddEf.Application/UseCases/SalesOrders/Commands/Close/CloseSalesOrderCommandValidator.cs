using FluentValidation;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Close;

public class CloseSalesOrderCommandValidator : AbstractValidator<CloseSalesOrderCommand>
{
    public CloseSalesOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty(); 
    }
}
