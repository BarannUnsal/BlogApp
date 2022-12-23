using System.ComponentModel.DataAnnotations;

namespace FealtedByEsra.PL.Models.User
{
    public class SignInUserViewModel
    {
        public string NameSurname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Telefon numaranızı boşluk bırakmadan yazınız.")]
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
