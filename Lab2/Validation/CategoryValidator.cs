using FluentValidation;
using CostAccounting.Entities;

namespace CostAccounting.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .MaximumLength(30);
        }        
    }
}
