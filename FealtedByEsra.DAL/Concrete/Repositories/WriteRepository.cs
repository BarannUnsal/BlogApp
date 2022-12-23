using FealtedByEsra.DAL.Abstract.Repositories;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FealtedByEsra.DAL.Concrete.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly FealtedByEsraDbContext _context;

        public WriteRepository(FealtedByEsraDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            await _context.SaveChangesAsync();
            return entityEntry.State == EntityState.Added;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T? model = await Table.FirstOrDefaultAsync(x => x.Id == id);
            await _context.SaveChangesAsync();
            if (model != null)
                return Remove(model);

            return false;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            _context.SaveChanges();
            return entityEntry.State == EntityState.Modified;
        }
    }
}
