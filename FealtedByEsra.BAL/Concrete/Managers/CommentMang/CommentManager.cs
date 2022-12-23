using FealtedByEsra.BAL.Abstract.Services.CommentService;
using FealtedByEsra.DAL.Abstract.Repositories.CommentRepo;
using FealtedByEsra.Entity.Entities;
using System.Linq.Expressions;

namespace FealtedByEsra.BAL.Concrete.Managers.CommentMang
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICommentWriteRepository _commentWriteRepository;

        public CommentManager(ICommentReadRepository commentReadRepository, ICommentWriteRepository commentWriteRepository)
        {
            _commentReadRepository = commentReadRepository;
            _commentWriteRepository = commentWriteRepository;
        }

        public async Task<bool> AddAsync(Comment model)
        {
            await _commentWriteRepository.AddAsync(model);
            await _commentWriteRepository.SaveAsync();
            return true;
        }

        public IQueryable<Comment> GetAll(bool tracking = true)
        {
            return _commentReadRepository.GetAll();
        }

        public Task<Comment> GetByIdAsync(int id, bool tracking = true)
        {
            return _commentReadRepository.GetByIdAsync(id);
        }

        public IQueryable<Comment> GetWhere(Expression<Func<Comment, bool>> method, bool tracking = true)
        {
            return _commentReadRepository.GetWhere(method);
        }

        public bool Remove(Comment model)
        {
            _commentWriteRepository.Remove(model);
            _commentWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await _commentWriteRepository.RemoveAsync(id);
            await _commentWriteRepository.SaveAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _commentWriteRepository.SaveAsync();
        }

        public bool Update(Comment model)
        {
            _commentWriteRepository.Update(model);
            _commentWriteRepository.SaveAsync();
            return true;
        }
    }
}
