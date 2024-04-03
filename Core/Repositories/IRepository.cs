using Core.Entities;

namespace Core.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetById(Guid id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
