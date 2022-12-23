using System.ComponentModel.DataAnnotations;

namespace FealtedByEsra.PL.Models.Comment
{
    public class CommentViewModel
    {
        public string? FirstAndLastName { get; set; }
        public int MainId { get; set; } 
        public string? Title { get; set; }
        [Required(ErrorMessage = "Lütfen yorum içeriği girin!")]
        public string Content { get; set; } = null!;
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir mail adresi girin!")]
        public string? Email { get; set; }
        public int PostId { get; set; }
        public string? Token { get; set; }
    }
}
