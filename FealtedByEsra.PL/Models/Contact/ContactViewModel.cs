namespace FealtedByEsra.PL.Models.Contact
{
    public class ContactViewModel
    {
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; } 
        public string Token { get; set; } = null!;
    }
}
