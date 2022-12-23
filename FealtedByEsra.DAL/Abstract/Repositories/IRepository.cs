using FealtedByEsra.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace FealtedByEsra.DAL.Abstract.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
