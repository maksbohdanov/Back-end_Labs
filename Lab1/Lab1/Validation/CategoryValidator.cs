using FluentValidation;
using Lab1.Entities;

namespace Lab1.Validation
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
