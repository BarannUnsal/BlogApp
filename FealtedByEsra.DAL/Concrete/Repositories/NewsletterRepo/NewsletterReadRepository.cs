using FealtedByEsra.DAL.Abstract.Repositories.NewsletterRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.NewsletterRepo
{
    public class NewsletterReadRepository : ReadRepository<Newsletter>, INewsLetterReadRepository
    {
        public NewsletterReadRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
