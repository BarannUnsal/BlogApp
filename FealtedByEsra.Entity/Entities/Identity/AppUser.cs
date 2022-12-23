using Microsoft.AspNetCore.Identity;

namespace FealtedByEsra.Entity.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; } = null!;
    }
}
