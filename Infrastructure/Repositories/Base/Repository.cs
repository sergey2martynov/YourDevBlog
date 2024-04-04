using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().OrderByDescending(p => p.CreatedOn);
        }

        public virtual IQueryable<T> GetById(Guid id)
        {
            return _dbContext.Set<T>()
                .Where(q => q.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
