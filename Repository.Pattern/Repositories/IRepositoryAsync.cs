using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern.Repositories
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        Task<TEntity?> FindAsync<TType>(TType keyValue);
        Task<bool> DeleteAsync<TType>(TType keyValue);
        //Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        //Task<TEntity> InsertAsync(TEntity entity);
        //Task DeleteAsync(TEntity entity);
        //Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter);
        //Task<IEnumerable<TEntity>> GetAllAsync();
        //Task<TEntity> GetByIdAsync(int id);
        //Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter = null,
        //                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //                                  int? top = null,
        //                                  int? skip = null,
        //                                  params string[] includeProperties);

    }
}