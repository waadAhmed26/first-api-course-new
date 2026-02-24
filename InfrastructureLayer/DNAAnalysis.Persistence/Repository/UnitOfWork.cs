using DNAAnalysis.Domain.Contracts;
using DNAAnalysis.Domain.Entities;
using DNAAnalysis.Persistence.Data.DBContexts;

namespace DNAAnalysis.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DNAAnalysisDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(DNAAnalysisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangeAsync()
            => await _dbContext.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey>
            GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out object? repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository!;
            }

            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[entityType] = newRepo;

            return newRepo;
        }
    }
}