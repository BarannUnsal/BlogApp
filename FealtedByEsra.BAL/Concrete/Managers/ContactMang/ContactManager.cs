using FealtedByEsra.BAL.Abstract.Services.ContactService;
using FealtedByEsra.DAL.Abstract.Repositories.ContactRepo;
using FealtedByEsra.Entity.Entities;
using System.Linq.Expressions;

namespace FealtedByEsra.BAL.Concrete.Managers.ContactMang
{
    public class ContactManager : IContactService
    {
        private readonly IContactReadRepository _contactReadRepository;
        private readonly IContactWriteRepository _contactWriteRepository;

        public ContactManager(IContactReadRepository contactReadRepository, IContactWriteRepository contactWriteRepository)
        {
            _contactReadRepository = contactReadRepository;
            _contactWriteRepository = contactWriteRepository;
        }

        public async Task<bool> AddAsync(Contact model)
        {
            await _contactWriteRepository.AddAsync(model);
            await _contactWriteRepository.SaveAsync();
            return true;
        }

        public IQueryable<Contact> GetAll(bool tracking = true)
        {
            return _contactReadRepository.GetAll();
        }

        public Task<Contact> GetByIdAsync(int id, bool tracking = true)
        {
            return _contactReadRepository.GetByIdAsync(id);
        }

        public IQueryable<Contact> GetWhere(Expression<Func<Contact, bool>> method, bool tracking = true)
        {
            return _contactReadRepository.GetWhere(method);
        }

        public bool Remove(Contact model)
        {
            _contactWriteRepository.Remove(model);
            _contactWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await _contactWriteRepository.RemoveAsync(id);
            await _contactWriteRepository.SaveAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _contactWriteRepository.SaveAsync();
        }

        public bool Update(Contact model)
        {
            _contactWriteRepository.Update(model);
            _contactWriteRepository.SaveAsync();
            return true;
        }
    }
}
