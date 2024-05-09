#region

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Pattern.DataContext;
using Repository.Pattern.EF;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace ProcedureRepository.Pattern.EF
{
    public class RawSqlRepository : IRawSqlRepository


    {
        #region Private Fields
         
        public readonly DbContext _dbContext;
        #endregion Private Fields

        public RawSqlRepository(IDataContextAsync context )
        { 
            _dbContext = context as DbContext;
        }

        public IEnumerable<TEntity>? QueryEnumerable<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            if (_dbContext != null)
            {
                var _dbSet = _dbContext.Set<TEntity>();

                return _dbSet.FromSqlRaw(query, parameters).AsEnumerable();
            }

            return null;
        }

        public TEntity? QueryFirst<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            if (_dbContext != null)
            {
                var _dbSet = _dbContext.Set<TEntity>();

                return _dbSet.FromSqlRaw(query, parameters).AsEnumerable().FirstOrDefault();
            }

            return null;
        }

        public List<TEntity>? QueryList<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            if (_dbContext != null)
            {
                var _dbSet = _dbContext.Set<TEntity>();

                return _dbSet.FromSqlRaw(query, parameters).AsEnumerable().ToList();
            }

            return null;
        }

        public virtual IQueryable<TEntity>? SelectQuery<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            if (_dbContext != null)
            {
                var _dbSet = _dbContext.Set<TEntity>();
                 
                return _dbSet.FromSqlRaw(query, parameters).AsQueryable();
            }

            return null;
          
        }

        // Not working due an issue in microsoft .net core https://github.com/dotnet/efcore/issues/18205
        //public virtual async Task<TEntity?> SelectFirstQueryAsync<TEntity>(string query, params object[] parameters) where TEntity : class
        //{
        //    if (_dbContext != null)
        //    {
        //        var _dbSet = _dbContext.Set<TEntity>();

        //        var result =  _dbSet.FromSqlRaw(query, parameters).AsQueryable();
        //        return result.FirstOrDefault();
        //    }

        //    return null;

        //}
        //public virtual async Task<List<TEntity>> SelectQueryAsync<TEntity>(string query, params object[] parameters) where TEntity : class
        //{
        //    if (_dbContext != null)
        //    {
        //        var _dbSet = _dbContext.Set<TEntity>();

        //        return await _dbSet.FromSqlRaw(query, parameters).AsQueryable().ToListAsync();
        //    }

        //    return null;

        //}
    }
}