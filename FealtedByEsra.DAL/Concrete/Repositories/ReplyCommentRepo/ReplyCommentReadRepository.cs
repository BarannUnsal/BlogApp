using FealtedByEsra.DAL.Abstract.Repositories.ReplyCommentRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FealtedByEsra.DAL.Concrete.Repositories.ReplyCommentRepo
{
    public class ReplyCommentReadRepository : ReadRepository<ReplyComment>, IReplyCommentReadRepository
    {
        public ReplyCommentReadRepository(FealtedByEsraDbContext context) : base(context)
        {
        }

        public async Task<ReplyComment> GetByIdReplyComment(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            var replyComment = await Table.AsQueryable().Include(p => p.Comment)!.ThenInclude(p => p.Post).FirstOrDefaultAsync(x => x.Id == id);

            if (replyComment == null)
                throw new Exception("Post bulunamadı");
            else
                return replyComment;
        }
    }
}
