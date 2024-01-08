using FluentValidation;

namespace DddEf.Application.UseCases.Customers.Commands
{
    public class CreateItemCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x=>x.CustomerCode).NotEmpty();
            RuleFor(x=>x.CustomerName).NotEmpty(); 
        }
    }
}
