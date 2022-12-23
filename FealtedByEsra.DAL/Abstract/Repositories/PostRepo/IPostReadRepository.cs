using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Abstract.Repositories.PostRepo
{
    public interface IPostReadRepository : IReadRepository<Post>
    {
        Task<IList<Post>> LastTwoPostAsync();
        Task<Post> GetPostIdAsync(int id, bool tracking = true);
        Task<IList<Post>> SearchPost(string text);
        Task<IList<Post>> TopThreePostAsync();
    }
}
