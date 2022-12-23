using FealtedByEsra.DAL.Abstract.Repositories.CategoryRepo;
using FealtedByEsra.DAL.Abstract.Repositories.CommentRepo;
using FealtedByEsra.DAL.Abstract.Repositories.ContactRepo;
using FealtedByEsra.DAL.Abstract.Repositories.NewsletterRepo;
using FealtedByEsra.DAL.Abstract.Repositories.PostRepo;
using FealtedByEsra.DAL.Abstract.Repositories.ReplyCommentRepo;
using FealtedByEsra.DAL.Concrete.Repositories.CategoryRepo;
using FealtedByEsra.DAL.Concrete.Repositories.CommentRepo;
using FealtedByEsra.DAL.Concrete.Repositories.ContactRepo;
using FealtedByEsra.DAL.Concrete.Repositories.NewsletterRepo;
using FealtedByEsra.DAL.Concrete.Repositories.PostRepo;
using FealtedByEsra.DAL.Concrete.Repositories.ReplyCommentRepo;
using Microsoft.Extensions.DependencyInjection;

namespace FealtedByEsra.DAL
{
    public static class ServiceRegistration
    {
        public static void AddDALService(this IServiceCollection services)
        {
            services.AddScoped<IPostWriteRepository, PostWriteRepository>();
            services.AddScoped<IPostReadRepository, PostReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICommentReadRepository, CommentReadRepository>();
            services.AddScoped<ICommentWriteRepository, CommentWriteRepository>();
            services.AddScoped<INewsLetterReadRepository, NewsletterReadRepository>();
            services.AddScoped<INewsletterWriteRepository, NewsletterWriteRepository>();
            services.AddScoped<IReplyCommentReadRepository, ReplyCommentReadRepository>();
            services.AddScoped<IReplyCommentWriteRepository, ReplyCommentWriteRepository>();
            services.AddScoped<IContactReadRepository, ContactReadRepository>();
            services.AddScoped<IContactWriteRepository, ContactWriteRepository>();
        }
    }
}
