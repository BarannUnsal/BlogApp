using FealtedByEsra.DAL.Abstract.Repositories.ContactRepo;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.DAL.Concrete.Repositories.ContactRepo
{
    public class ContactWriteRepository : WriteRepository<Contact>, IContactWriteRepository
    {
        public ContactWriteRepository(FealtedByEsraDbContext context) : base(context)
        {
        }
    }
}
