using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.BAL.DTOs
{
    public class PostResponse
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
