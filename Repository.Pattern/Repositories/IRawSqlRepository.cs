using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern.Repositories
{
    public interface IRawSqlRepository
    {
        IQueryable<TEntity>? SelectQuery<TEntity>(string query, params object[] parameters) where TEntity : class;
        List<TEntity>? QueryList<TEntity>(string query, params object[] parameters) where TEntity : class;
        IEnumerable<TEntity>? QueryEnumerable<TEntity>(string query, params object[] parameters) where TEntity : class;
        TEntity? QueryFirst<TEntity>(string query, params object[] parameters) where TEntity : class;
        //Task<List<TEntity>> SelectQueryAsync<TEntity>(string query, params object[] parameters) where TEntity : class;
        // Task<TEntity?> SelectFirstQueryAsync<TEntity>(string query, params object[] parameters) where TEntity : class;
    }
}