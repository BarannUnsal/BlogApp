using FealtedByEsra.DAL.Abstract.Repositories.PostRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.PostRepo
{
    public class PostWriteRepository : WriteRepository<Post>, IPostWriteRepository
    {
        public PostWriteRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
