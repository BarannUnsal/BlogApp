using System.ComponentModel.DataAnnotations;

namespace FealtedByEsra.PL.Models.User
{
    public class PasswordChangeAdminViewModel
    {
        [Required(ErrorMessage = "Eski şifrenizi giriniz")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Şifrenizi giriniz")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Şifrenizi doğrulayın")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
