using Core.Entities;

namespace Core.Repositories
{
    public interface IRepository<T > where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IQueryable<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
