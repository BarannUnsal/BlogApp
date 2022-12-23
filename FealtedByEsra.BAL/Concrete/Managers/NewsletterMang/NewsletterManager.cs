using FealtedByEsra.BAL.Abstract.Services;
using FealtedByEsra.DAL.Abstract.Repositories.NewsletterRepo;
using FealtedByEsra.Entity.Entities;
using System.Linq.Expressions;

namespace FealtedByEsra.BAL.Concrete.Managers.NewsletterMang
{
    public class NewsletterManager : INewsletterService
    {
        private readonly INewsLetterReadRepository _newsLetterReadRepository;
        private readonly INewsletterWriteRepository _newsletterWriteRepository;

        public NewsletterManager(INewsLetterReadRepository newsLetterReadRepository, INewsletterWriteRepository newsletterWriteRepository)
        {
            _newsLetterReadRepository = newsLetterReadRepository;
            _newsletterWriteRepository = newsletterWriteRepository;
        }

        public async Task<bool> AddAsync(Newsletter model)
        {
            await _newsletterWriteRepository.AddAsync(model);
            await _newsletterWriteRepository.SaveAsync();
            return true;
        }

        public IQueryable<Newsletter> GetAll(bool tracking = true)
        {
            return _newsLetterReadRepository.GetAll();
        }

        public async Task<Newsletter> GetByIdAsync(int id, bool tracking = true)
        {
            return await _newsLetterReadRepository.GetByIdAsync(id);
        }

        public IQueryable<Newsletter> GetWhere(Expression<Func<Newsletter, bool>> method, bool tracking = true)
        {
            return _newsLetterReadRepository.GetWhere(method);
        }

        public bool Remove(Newsletter model)
        {
            _newsletterWriteRepository.Remove(model);
            _newsletterWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await _newsletterWriteRepository.RemoveAsync(id);
            await _newsletterWriteRepository.SaveAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _newsletterWriteRepository.SaveAsync();
        }

        public bool Update(Newsletter model)
        {
            _newsletterWriteRepository.Update(model);
            _newsletterWriteRepository.SaveAsync();
            return true;
        }
    }
}
