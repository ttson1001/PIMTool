using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();

        Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        void Update(T entity);

        void Delete(List<T> entities);

        void ClearChangeTracking();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<List<T>?> GetValuesAsync(CancellationToken cancellationToken = default);
    }
}