using FluentValidation;
using Lab1.Entities;

namespace Lab1.Validation
{
    public class RecordValidator : AbstractValidator<Record>
    {
        public RecordValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty();

            RuleFor(x => x.Sum)
               .NotEmpty();
        }
    }
}
