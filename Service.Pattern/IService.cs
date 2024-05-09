using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;

using Repository.Pattern.Repositories;
namespace Service.Pattern
{
    public interface IService<TEntity, TModel> where TEntity : IObjectState
    {
        TModel Find<TType>(TType keyValue);
        IQueryable<TModel> SelectQuery(string query, params object[] parameters);
        void Insert(TModel model);
        //Task<int> InsertAsync(TModel model);
        TEntity InsertAndReturn(TModel model);
        TModel InsertAndReturnModel(TModel model);
        //Task<TModel> InsertAndReturnModelAsync(TModel model);
        void InsertRange(IEnumerable<TModel> models);
        void InsertOrUpdateGraph(TModel model);
        void InsertGraphRange(IEnumerable<TModel> models);
        void Update(TModel entity);
        void Delete<TType>(TType id); 
        void Delete(Expression<Func<TEntity, bool>> query);
        IQueryFluent<TEntity> Query();
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        List<TModel> QueryModel(List<Expression<Func<TEntity, object>>> includes = null);
        //Task<List<TModel>> QueryModelAsync(List<Expression<Func<TEntity, object>>> includes = null);

        List<TModel> QueryModel(IQueryObject<TEntity> queryObject, List<Expression<Func<TEntity, object>>> includes = null);
        List<TModel> QueryModel(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null);

        TModel QueryFirstModel(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>>? includes = null);

        //Task<List<TModel>> QueryModelAsync(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null);

        //Task<TModel> QueryFirstModelAsync(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null);

       // Task<TModel> QueryLastModelAsync(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null);

       // Task<TModel> FindAsync<TType>(TType keyValue);

       // Task<bool> DeleteAsync<TType>(TType keyValue); 
        List<TModel> Queryable();
        void RejectChanges();
    }
}