using FealtedByEsra.DAL.Abstract.Repositories;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FealtedByEsra.DAL.Concrete.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly FealtedByEsraDbContext _context;

        public ReadRepository(FealtedByEsraDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();
            
            var q = await query.FirstOrDefaultAsync(x => x.Id == id);
            
            if (q != null)
                return q;

            throw new Exception("Bulunamadı!");
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}
