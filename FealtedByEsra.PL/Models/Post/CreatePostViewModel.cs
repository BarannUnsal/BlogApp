namespace FealtedByEsra.PL.Models.Post
{
    public class CreatePostViewModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Thumbnail { get; set; } = null!;
        public DateTime CreatedDate{ get; set; }
        public int CategoryId { get; set; }
    }
}
