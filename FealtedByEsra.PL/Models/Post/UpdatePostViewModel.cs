namespace FealtedByEsra.PL.Models.Post
{
    public class UpdatePostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Thumbnail { get; set; } = null!;
        public string? ExistingThumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
