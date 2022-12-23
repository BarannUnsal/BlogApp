using FealtedByEsra.DAL.Abstract.Repositories.ContactRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.ContactRepo
{
    public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
    {
        public ContactReadRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
