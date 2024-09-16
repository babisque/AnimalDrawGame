namespace BetService.Core.Repositories;

public interface IRepository<T> where T : class
{
    Task<IList<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}