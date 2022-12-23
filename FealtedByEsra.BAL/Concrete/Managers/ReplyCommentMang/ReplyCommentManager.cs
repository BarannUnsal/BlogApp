using FealtedByEsra.BAL.Abstract.Services.ReplyService;
using FealtedByEsra.DAL.Abstract.Repositories.ReplyCommentRepo;
using FealtedByEsra.Entity.Entities;
using System.Linq.Expressions;

namespace FealtedByEsra.BAL.Concrete.Managers.ReplyCommentMang
{
    public class ReplyCommentManager : IReplyCommentService
    {
        private readonly IReplyCommentReadRepository _replyCommentReadRepository;
        private readonly IReplyCommentWriteRepository _replyCommentWriteRepository;

        public ReplyCommentManager(IReplyCommentReadRepository replyCommentReadRepository, IReplyCommentWriteRepository replyCommentWriteRepository)
        {
            _replyCommentReadRepository = replyCommentReadRepository;
            _replyCommentWriteRepository = replyCommentWriteRepository;
        }

        public async Task<bool> AddAsync(ReplyComment model)
        {
            await _replyCommentWriteRepository.AddAsync(model);
            await _replyCommentWriteRepository.SaveAsync();
            return true;
        }

        public IQueryable<ReplyComment> GetAll(bool tracking = true)
        {
            return _replyCommentReadRepository.GetAll();
        }

        public Task<ReplyComment> GetByIdAsync(int id, bool tracking = true)
        {
            return _replyCommentReadRepository.GetByIdReplyComment(id);
        }

        public IQueryable<ReplyComment> GetWhere(Expression<Func<ReplyComment, bool>> method, bool tracking = true)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ReplyComment model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await _replyCommentWriteRepository.RemoveAsync(id);
            await _replyCommentWriteRepository.SaveAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _replyCommentWriteRepository.SaveAsync();
        }

        public bool Update(ReplyComment model)
        {
            throw new NotImplementedException();
        }
    }
}
