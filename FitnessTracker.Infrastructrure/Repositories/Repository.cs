
using FitnessTracker.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructrure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly FitnessDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(FitnessDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbSet.ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(id, ct);
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public void Update(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _dbSet.FindAsync(id, ct);
            if (entity != null)
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}
