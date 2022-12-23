using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.BAL.DTOs
{
    public class CategoryResponse
    {
        public List<Category> Posts { get; set; } = new List<Category>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
