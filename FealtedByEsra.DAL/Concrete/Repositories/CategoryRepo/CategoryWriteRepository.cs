using FealtedByEsra.DAL.Abstract.Repositories.CategoryRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.CategoryRepo
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
