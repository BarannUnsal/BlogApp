using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.BAL.DTOs
{
    public class ContactRepsonse
    {
        public List<Contact> Posts { get; set; } = new List<Contact>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
