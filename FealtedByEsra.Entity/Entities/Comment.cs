using FealtedByEsra.Entity.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace FealtedByEsra.Entity.Entities
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            ReplyComments = new HashSet<ReplyComment>();
            Post = new Post();
        }
        public string? FirstAndLastName { get; set; }
        [Required(ErrorMessage = "Lütfen yorum başlığı girin")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Lütfen yorum içeriği girin")]
        public string Content { get; set; } = null!;
        public string? Email { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } 
        public ICollection<ReplyComment>? ReplyComments{ get; set; } 
    }
}
