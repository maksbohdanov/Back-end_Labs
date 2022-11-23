using FluentValidation;
using CostAccounting.Entities;

namespace CostAccounting.Validation
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
