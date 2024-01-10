using FluentValidation;

namespace DddEf.Application.UseCases.Customers.Commands;

public class AddItemCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(x=>x.CustomerCode).NotEmpty();
        RuleFor(x=>x.CustomerName).NotEmpty(); 
    }
}
