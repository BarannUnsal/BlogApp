using FealtedByEsra.DAL.Abstract.Repositories.CategoryRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.CategoryRepo
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
