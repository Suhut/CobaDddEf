using FluentValidation;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Cancel;

public class CancelSalesOrderCommandValidator : AbstractValidator<CancelSalesOrderCommand>
{
    public CancelSalesOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty(); 
    }
}
