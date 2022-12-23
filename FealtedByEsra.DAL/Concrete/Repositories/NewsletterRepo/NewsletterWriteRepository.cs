using FealtedByEsra.DAL.Abstract.Repositories.NewsletterRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.NewsletterRepo
{
    public class NewsletterWriteRepository : WriteRepository<Newsletter>, INewsletterWriteRepository
    {
        public NewsletterWriteRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
