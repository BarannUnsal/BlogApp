using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.BAL.DTOs
{
    public class CommentResponse
    {
        public List<Comment> Posts { get; set; } = new List<Comment>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
