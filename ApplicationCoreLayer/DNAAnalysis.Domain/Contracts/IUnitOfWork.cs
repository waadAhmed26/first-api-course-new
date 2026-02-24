using DNAAnalysis.Domain.Entities;

namespace DNAAnalysis.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();

        IGenericRepository<TEntity, TKey>
            GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>;
    }
}