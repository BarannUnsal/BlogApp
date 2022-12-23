using FealtedByEsra.DAL.Abstract.Repositories.CommentRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.CommentRepo
{
    public class CommentReadRepository : ReadRepository<Comment>, ICommentReadRepository
    {
        public CommentReadRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
