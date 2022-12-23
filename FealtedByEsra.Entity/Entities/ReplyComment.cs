using FealtedByEsra.Entity.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace FealtedByEsra.Entity.Entities
{
    public class ReplyComment : BaseEntity
    {
        public string? FirstAndLastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Lütfen yorum içeriği girin")]
        public string Content { get; set; } = null!;
        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!; 
    }
}
