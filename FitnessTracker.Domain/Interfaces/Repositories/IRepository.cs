
namespace FitnessTracker.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
