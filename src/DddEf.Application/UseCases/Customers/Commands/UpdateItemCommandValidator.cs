using FluentValidation;

namespace DddEf.Application.UseCases.Customers.Commands;

public class UpdateItemCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty();
        RuleFor(x=>x.CustomerCode).NotEmpty();
        RuleFor(x=>x.CustomerName).NotEmpty(); 
    }
}
