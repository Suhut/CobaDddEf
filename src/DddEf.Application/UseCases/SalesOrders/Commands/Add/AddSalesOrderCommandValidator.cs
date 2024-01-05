using FluentValidation;

namespace DddEf.Application.UseCases.SalesOrders.Commands.Add
{
    public class AddSalesOrderCommandValidator : AbstractValidator<AddSalesOrderCommand>
    {
        public AddSalesOrderCommandValidator()
        {
            RuleFor(x => x.TransNo).NotEmpty();
            RuleFor(x => x.TransDate).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.Items).NotEmpty();
        }
    }
}
