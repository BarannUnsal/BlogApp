using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FealtedByEsra.PL.Models.User
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz")]
        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
