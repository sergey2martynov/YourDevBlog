using Core.Entities;

namespace Core.Repositories
{
    public interface IRepository<T > where T : EntityBase
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetById(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
