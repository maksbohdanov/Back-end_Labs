using FluentValidation;
using CostAccounting.Entities;

namespace CostAccounting.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
        }        
    }
}
