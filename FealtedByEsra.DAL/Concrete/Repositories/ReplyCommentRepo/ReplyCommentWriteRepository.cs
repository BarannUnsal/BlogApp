using FealtedByEsra.DAL.Abstract.Repositories.ReplyCommentRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.ReplyCommentRepo
{
    public class ReplyCommentWriteRepository : WriteRepository<ReplyComment>, IReplyCommentWriteRepository
    {
        public ReplyCommentWriteRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
