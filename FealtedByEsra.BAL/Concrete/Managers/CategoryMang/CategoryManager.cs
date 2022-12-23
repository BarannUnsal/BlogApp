using FealtedByEsra.BAL.Abstract.Services.CategoryService;
using FealtedByEsra.DAL.Abstract.Repositories.CategoryRepo;
using FealtedByEsra.Entity.Entities;
using System.Linq.Expressions;

namespace FealtedByEsra.BAL.Concrete.Managers.CategoryMang
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        public CategoryManager(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }
        public async Task<bool> AddAsync(Category model)
        {
            await _categoryWriteRepository.AddAsync(model);
            await _categoryWriteRepository.SaveAsync();
            return true;
        }

        public IQueryable<Category> GetAll(bool tracking = true)
        {
            return _categoryReadRepository.GetAll();
        }

        public async Task<Category> GetByIdAsync(int id, bool tracking = true)
        {

            return await _categoryReadRepository.GetByIdAsync(id);
        }

        public IQueryable<Category> GetWhere(Expression<Func<Category, bool>> method, bool tracking = true)
        {
            return _categoryReadRepository.GetWhere(method);
        }

        public bool Remove(Category model)
        {
            _categoryWriteRepository.Remove(model);
            _categoryWriteRepository.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await _categoryWriteRepository.RemoveAsync(id);
            await _categoryWriteRepository.SaveAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _categoryWriteRepository.SaveAsync();
        }

        public bool Update(Category model)
        {
            _categoryWriteRepository.Update(model);
            _categoryWriteRepository.SaveAsync();
            return true;
        }
    }
}
