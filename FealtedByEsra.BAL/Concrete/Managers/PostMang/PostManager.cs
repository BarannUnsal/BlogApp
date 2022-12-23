using FealtedByEsra.BAL.Abstract.Services.PostService;
using FealtedByEsra.DAL.Abstract.Repositories.PostRepo;
using FealtedByEsra.Entity.Entities;
using System.Linq.Expressions;

namespace FealtedByEsra.BAL.Concrete.Managers.PostMang
{
    public class PostManager : IPostService
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        public PostManager(IPostReadRepository postReadRepository, IPostWriteRepository postWriteRepository)
        {
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
        }

        public async Task<bool> AddAsync(Post model)
        {
            await _postWriteRepository.AddAsync(model);
            await _postWriteRepository.SaveAsync();
            return true;
        }

        public IQueryable<Post> GetAll(bool tracking = true)
        {
            return _postReadRepository.GetAll();
        }

        public async Task<Post> GetByIdAsync(int id, bool tracking = true)
        {
            return await _postReadRepository.GetByIdAsync(id);
        }

        public IList<Post> GetPostByCategory(int? category = null, bool tracking = true)
        {
            if (category != null)
                return _postReadRepository.GetAll(false).OrderByDescending(p => p.Id).Where(p => p.CategoryId == category).ToList();
            else
                return _postReadRepository.GetAll(false).ToList();
        }

        public async Task<Post> GetPostIdAsync(int id, bool tracking = true)
        {
            return await _postReadRepository.GetByIdAsync(id);
        }

        public IQueryable<Post> GetWhere(Expression<Func<Post, bool>> method, bool tracking = true)
        {
            return _postReadRepository.GetWhere(method);
        }

        public bool Remove(Post model)
        {
            _postWriteRepository.Remove(model);
            _postWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await _postWriteRepository.RemoveAsync(id);
            await _postWriteRepository.SaveAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _postWriteRepository.SaveAsync();
        }

        public async Task<IList<Post>> SearchPost(string text)
        {
            return await _postReadRepository.SearchPost(text);
        }

        public async Task<IList<Post>> TakeLastTwoPostAsync()
        {
            return await _postReadRepository.LastTwoPostAsync();
        }

        public async Task<IList<Post>> TopThreePostAsync()
        {
            return await _postReadRepository.TopThreePostAsync();
        }

        public bool Update(Post model)
        {
            _postWriteRepository.Update(model);
            _postWriteRepository.SaveAsync();
            return true;
        }
    }
}
