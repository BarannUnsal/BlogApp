using FealtedByEsra.Entity.Entities;
using FluentValidation;

namespace FealtedByEsra.BAL.Validations.ContactValidate
{
    public class ContactValidation : AbstractValidator<Contact>
    {
        public ContactValidation()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Başlık kısmı boş olamaz").NotNull().WithMessage("Başlık kısmı boş olamaz").Length(2, 150).WithMessage("2 - 150 karakter arasında olmalıdır.");

            RuleFor(c => c.Description).NotNull().WithMessage("Lütfen mesaj içeriği giriniz").NotEmpty().WithMessage("Lütfen mesaj içeriği giriniz").MinimumLength(2);

            RuleFor(c => c.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz").NotNull().WithMessage("Lütfen email adresinizi girin.").NotEmpty().WithMessage("Lütfen email adresinizi girin.");
        }
    }
}
