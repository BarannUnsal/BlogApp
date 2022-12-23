using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.BAL.Abstract.Services.PostService
{
    public interface IPostService : IGenericService<Post>
    {
        Task<IList<Post>> TakeLastTwoPostAsync();
        Task<Post> GetPostIdAsync(int id, bool tracking = true);
        Task<IList<Post>> SearchPost(string text);
        Task<IList<Post>> TopThreePostAsync();
        IList<Post> GetPostByCategory(int? category, bool tracking = true);
    }
}
