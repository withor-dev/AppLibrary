using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces;

public interface IBaseRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}