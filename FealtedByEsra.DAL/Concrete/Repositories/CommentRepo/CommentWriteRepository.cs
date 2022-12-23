using FealtedByEsra.DAL.Abstract.Repositories.CommentRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.CommentRepo
{
    public class CommentWriteRepository : WriteRepository<Comment>, ICommentWriteRepository
    {
        public CommentWriteRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
