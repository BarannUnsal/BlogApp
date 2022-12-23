using FealtedByEsra.DAL.Abstract.Repositories.PostRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FealtedByEsra.DAL.Concrete.Repositories.PostRepo
{
    public class PostReadRepository : ReadRepository<Post>, IPostReadRepository
    {
        private readonly FealtedByEsraDbContext context;

        public PostReadRepository(FealtedByEsraDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Post> GetPostIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            var post = await Table.AsQueryable().Include(p => p.Comments)!.ThenInclude(p => p.ReplyComments).FirstOrDefaultAsync(x => x.Id == id);

            if(post == null)
                throw new Exception("Post bulunamadı");
            else
                return post;
        }

        public async Task<IList<Post>> LastTwoPostAsync()
        {
            var lastTwoPost = await context!.Posts!.AsNoTracking().OrderByDescending(x => x.Id).Take(2).ToListAsync();
            if (lastTwoPost == null)
                throw new Exception("Son 2 post bulunamadı");
            else
                return lastTwoPost;
        }

        public async Task<IList<Post>> SearchPost(string text)
        {
            text = text.ToLower();
            var posts = await context!.Posts!.AsNoTracking().Where(p => p.Title.ToLower().Contains(text)).ToListAsync();
            return posts;
        }

        public async Task<IList<Post>> TopThreePostAsync()
        {
            var topThreePost = await context!.Posts!.AsNoTracking().OrderBy(x => x.Comments!.Count()).Take(3).ToListAsync();
            return topThreePost;
        }
    }
}
