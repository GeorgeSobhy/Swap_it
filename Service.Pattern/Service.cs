using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Service.Pattern
{
    public class Service<TEntity, TModel> : IService<TEntity, TModel> where TEntity : class, IObjectState
    {
        #region Private Fields
        private readonly IRepositoryAsync<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWork;
        #endregion Private Fields

        #region Constructor
        public Service(IRepositoryAsync<TEntity> repository, IMapper mapper, IUnitOfWorkAsync unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion Constructor

        public virtual TModel Find<TType>(TType keyValues) { return _mapper.Map<TModel>(_repository.Find(keyValues)); }

        public virtual IQueryable<TModel> SelectQuery(string query, params object[] parameters) { return _mapper.Map<IQueryable<TModel>>(_repository.SelectQuery(query, parameters).AsQueryable()); }

        public virtual void Insert(TModel model)
        {
            _repository.Insert(_mapper.Map<TEntity>(model));
            _unitOfWork.SaveChanges();


        }

        //public async Task<int> InsertAsync(TModel model)
        //{
        //    var entityToInsert = _mapper.Map<TEntity>(model);
        //    _repository.Insert(entityToInsert);
        //    var returnValue = await _unitOfWork.SaveChangesAsync();
        //    return returnValue;
        //}

        public virtual void InsertRange(IEnumerable<TModel> models)
        {
            _repository.InsertRange(_mapper.Map<IEnumerable<TEntity>>(models));
            _unitOfWork.SaveChanges();
        }

        public virtual void InsertOrUpdateGraph(TModel model)
        {
            _repository.InsertOrUpdateGraph(_mapper.Map<TEntity>(model));
            _unitOfWork.SaveChanges();
        }

        public virtual void InsertGraphRange(IEnumerable<TModel> models)
        {
            _repository.InsertGraphRange(_mapper.Map<IEnumerable<TEntity>>(models));
            _unitOfWork.SaveChanges();
        }

        public virtual void Update(TModel model)
        {
            _repository.Update(_mapper.Map<TEntity>(model));
            _unitOfWork.SaveChanges();
        }

        //public virtual async Task<int> UpdateAsync(TModel model)
        //{
        //    _repository.Update(_mapper.Map<TEntity>(model));
        //    var returnValue = await _unitOfWork.SaveChangesAsync();
        //    return returnValue;
        //}

        public virtual void Delete<TType>(TType id)
        {
            _repository.Delete(id);
            _unitOfWork.SaveChanges();
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> query)
        {
            var result = _repository.Query(query).Select();
            foreach (TEntity _item in result)
            {
                _repository.Delete(_item);
            }

            _unitOfWork.SaveChanges();
        }

 

        public IQueryFluent<TEntity> Query() { return _repository.Query(); }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject) { return _repository.Query(queryObject); }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query) { return _repository.Query(query); }

        //public virtual async Task<TModel> FindAsync<TType>(TType keyValue) { return _mapper.Map<TModel>(await _repository.FindAsync(keyValue)); }


        //public virtual async Task<bool> DeleteAsync<TType>(TType keyValue)
        //{
        //    var result = await _repository.DeleteAsync(keyValue);
        //    await _unitOfWork.SaveChangesAsync();
        //    return result;
        //}

        //public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        //{
        //    var result = await _repository.DeleteAsync(cancellationToken, keyValues);
        //    await _unitOfWork.SaveChangesAsync();
        //    return result;
        //}

        public List<TModel> Queryable()
        {

            var result = _repository.Queryable();

            var res = _mapper.Map<List<TModel>>(result.ToList());
            return res;
        }

        public List<TModel> QueryModel(List<Expression<Func<TEntity, object>>> includes = null)
        {
            if (includes != null)
            {
                var _resquery = _repository.Query();
                foreach (var include in includes)
                {
                    _resquery.Include(include);
                }

                return _mapper.Map<List<TModel>>(_resquery.Select().ToList());

            }
            else
            {


                var result = _repository.Query().Select();
                return _mapper.Map<List<TModel>>(result.ToList());
            }


        }

        public List<TModel> QueryModel(IQueryObject<TEntity> queryObject, List<Expression<Func<TEntity, object>>> includes = null)
        {


            if (includes != null)
            {
                var query = _repository.Query(queryObject);
                foreach (var include in includes)
                {
                    query.Include(include);
                }

                return _mapper.Map<List<TModel>>(query.Select().ToList());

            }
            else
            {
                var result = _repository.Query(queryObject).Select();
                return _mapper.Map<List<TModel>>(result.ToList());
            }


        }

        public List<TModel> QueryModel(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null)
        {


            if (includes != null)
            {
                var _resquery = _repository.Query(query);
                foreach (var include in includes)
                {
                    _resquery.Include(include);
                }
                var res = _resquery.Select().ToList();
                return _mapper.Map<List<TModel>>(res);

            }
            else
            {

                var result = _repository.Query(query).Select();
                //var entities = result.ToList();
                return _mapper.Map<List<TModel>>(result.ToList());
            }


        }


        public TEntity InsertAndReturn(TModel model)
        {

            var entity = _mapper.Map<TEntity>(model);
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
            return entity;
        }

        public TModel InsertAndReturnModel(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
            return _mapper.Map<TModel>(entity);
        }

        public void RejectChanges()
        {
            _unitOfWork.RejectChanges();
        }

        public TModel QueryFirstModel(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null)
        {
            if (includes != null)
            {
                var _resquery = _repository.Query(query);
                foreach (var include in includes)
                {
                    _resquery.Include(include);
                }
                var res = _resquery.Select();
                return _mapper.Map<TModel>(res.FirstOrDefault());

            }
            else
            {

                var result = _repository.Query(query).Select();
                //var entities = result.ToList();
                return _mapper.Map<TModel>(result.FirstOrDefault());
            }
        }

        //public async Task<List<TModel>> QueryModelAsync(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null)
        //{


        //    if (includes != null)
        //    {
        //        var _resquery = _repository.Query(query);
        //        foreach (var include in includes)
        //        {
        //            _resquery.Include(include);
        //        }
        //        var res = await _resquery.SelectAsync();
        //        return _mapper.Map<List<TModel>>(res);

        //    }
        //    else
        //    {

        //        var result = await _repository.Query(query).SelectAsync();
        //        //var entities = result.ToList();
        //        return _mapper.Map<List<TModel>>(result.ToList());
        //    }


        //}

        //public async Task<List<TModel>> QueryModelAsync(List<Expression<Func<TEntity, object>>> includes = null)
        //{
        //    if (includes != null)
        //    {
        //        var _resquery = _repository.Query();
        //        foreach (var include in includes)
        //        {
        //            _resquery.Include(include);
        //        }
        //        return _mapper.Map<List<TModel>>(await _resquery.SelectAsync());
        //    }
        //    else
        //    {
        //        return _mapper.Map<List<TModel>>(_repository.Query().SelectAsync());
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        //public async Task<TModel> QueryFirstModelAsync(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>>? includes)
        //{
        //    if (includes != null)
        //    {
        //        var resquery = _repository.Query(query);
        //        foreach (var include in includes)
        //        {
        //            resquery.Include(include);
        //        }
        //        var res = await resquery.SelectAsync();
        //        return _mapper.Map<TModel>(res.FirstOrDefault());

        //    }

        //    var result = await _repository.Query(query).SelectAsync();
        //    return _mapper.Map<TModel>(result.FirstOrDefault());
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        //public async Task<TModel> QueryLastModelAsync(Expression<Func<TEntity, bool>> query, List<Expression<Func<TEntity, object>>> includes = null)
        //{
        //    if (includes != null)
        //    {
        //        var resquery = _repository.Query(query);
        //        foreach (var include in includes)
        //        {
        //            resquery.Include(include);
        //        }
        //        var res = await resquery.SelectAsync();
        //        return _mapper.Map<TModel>(res.LastOrDefault());

        //    }

        //    var result = await _repository.Query(query).SelectAsync();
        //    return _mapper.Map<TModel>(result.LastOrDefault());
        //}

        //public async Task<TModel> InsertAndReturnModelAsync(TModel model)
        //{
        //    var entity = _mapper.Map<TEntity>(model);
        //    await _repository.InsertAsync(entity);
        //    await _unitOfWork.SaveChangesAsync();
        //    return _mapper.Map<TModel>(entity);
        //}

        //public async Task<int> InsertAsync(TModel model)
        //{
        //    await _repository.InsertAsync(_mapper.Map<TEntity>(model));
        //    return await _unitOfWork.SaveChangesAsync();


        //}
    }
}