using FluentValidation;

namespace DddEf.Application.UseCases.Items.Commands
{
    public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
    {
        public AddItemCommandValidator()
        {
            RuleFor(x=>x.ItemCode).NotEmpty();
            RuleFor(x=>x.ItemName).NotEmpty(); 
        }
    }
}
