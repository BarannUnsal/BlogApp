using FealtedByEsra.Entity.Entities.Common;

namespace FealtedByEsra.Entity.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }
        
        public string CategoryName { get; set; } = null!;

        public ICollection<Post>? Posts{ get; set; }
    }
}
