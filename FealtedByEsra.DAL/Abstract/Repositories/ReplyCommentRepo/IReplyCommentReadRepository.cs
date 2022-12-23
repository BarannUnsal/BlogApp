using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Abstract.Repositories.ReplyCommentRepo
{
    public interface IReplyCommentReadRepository : IReadRepository<ReplyComment>
    {
        Task<ReplyComment> GetByIdReplyComment(int id, bool tracking = true);
    }
}
