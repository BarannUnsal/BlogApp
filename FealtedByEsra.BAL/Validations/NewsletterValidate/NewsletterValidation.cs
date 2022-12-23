using FealtedByEsra.Entity.Entities;
using FluentValidation;

namespace FealtedByEsra.BAL.Validations.NewsletterValidate
{
    public class NewsletterValidation : AbstractValidator<Newsletter>
    {
        public NewsletterValidation()
        {
            RuleFor(n => n.Email).EmailAddress().NotEmpty().WithMessage("Email adresinizi kontrol edin!");
        }
    }
}
