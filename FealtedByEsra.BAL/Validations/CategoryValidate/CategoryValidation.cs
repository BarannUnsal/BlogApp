using FealtedByEsra.Entity.Entities;
using FluentValidation;

namespace FealtedByEsra.BAL.Validations.CategoryValidate
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.CategoryName).NotNull().WithMessage("Lütfen bir başlık giriniz!")
                .NotEmpty().WithMessage("Lütfen bir başlık giriniz!")
                .Length(3, 100).WithMessage("Lütfen 3 - 100 karakter arası başlık giriniz!");
        }
    }
}
