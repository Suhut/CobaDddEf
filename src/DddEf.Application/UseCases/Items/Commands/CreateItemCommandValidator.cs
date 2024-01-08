using FluentValidation;

namespace DddEf.Application.UseCases.Items.Commands
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x=>x.ItemCode).NotEmpty();
            RuleFor(x=>x.ItemName).NotEmpty(); 
        }
    }
}
