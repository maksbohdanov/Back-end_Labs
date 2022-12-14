using FluentValidation;
using WebApi.Models;

namespace CostAccounting.Validation
{
    public class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
               .NotEmpty()
               .MinimumLength(6);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
        }        
    }
}
