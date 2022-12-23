using FealtedByEsra.Entity.Entities;
using FluentValidation;

namespace FealtedByEsra.BAL.Validations.PostValidate
{
    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor(p => p.Title).NotNull().WithMessage("Lütfen bir başlık giriniz!")
                .NotEmpty().WithMessage("Lütfen bir başlık giriniz!")
                .Length(5, 100).WithMessage("Lütfen 3 - 100 karakter arası başlık giriniz!");

            RuleFor(p => p.Description).NotNull().WithMessage("Lütfen yazı giriniz!")
                .NotEmpty().WithMessage("Lütfen yazı giriniz!")
                .MinimumLength(10).WithMessage("Lütfen 10 karakterden fazla bir yazı giriniz!");

            RuleFor(p => p.Thumbnail).NotNull().WithMessage("Lütfen görsel ekleyiniz!")
                .NotEmpty().WithMessage("Lütfen görsel ekleyiniz!");
        }
    }
}
