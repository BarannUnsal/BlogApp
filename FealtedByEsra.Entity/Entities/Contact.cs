using FealtedByEsra.Entity.Entities.Common;

namespace FealtedByEsra.Entity.Entities
{
    public class Contact : BaseEntity
    {
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
