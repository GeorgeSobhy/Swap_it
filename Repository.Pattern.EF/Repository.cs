#region

using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Repository.Pattern.EF
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IObjectState

    {
        #region Private Fields

        private readonly IDataContextAsync _context;
        public readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion Private Fields

        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            // Temporarily for FakeDbContext, Unit Test and Fakes
            _dbContext = context as DbContext;

            if (_dbContext != null)
            {
                _dbSet = _dbContext.Set<TEntity>();
            }

        }

        public virtual TEntity? Find<TType>(TType keyValue)
        {
            return _dbSet.Find(keyValue);
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {

            entity.ObjectState = ObjectState.Added;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }



        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }


        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("Cannot add a null entity.");
            var _entry = _dbContext.Entry(entity);

            if (_entry.State == EntityState.Detached)
            {
                var _id = FindPrimaryKeys(_dbContext.Entry(entity));
                TEntity attachedEntity = Find(_id.FirstOrDefault());
                if (attachedEntity != null)
                {
                    var _attachedentry = _dbContext.Entry(attachedEntity);
                    _attachedentry.CurrentValues.SetValues(entity);
                    attachedEntity.ObjectState = ObjectState.Modified;
                    //_dbSet.Attach(attachedEntity);
                    _context.SyncObjectState(attachedEntity);
                }
                else
                {
                    _entry.State = EntityState.Modified;

                }
                //entity.ObjectState = ObjectState.Modified;
                //_dbSet.Attach(entity);
                //_context.SyncObjectState(entity);

            }
            else
            {
            }
        }

        public virtual void Delete<TType>(TType id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var _entry = _dbContext.Entry(entity); 
            entity.ObjectState = ObjectState.Deleted;
            _entry.State = EntityState.Deleted;
             
        }
        private List<object?>? FindPrimaryKeys(EntityEntry entry)
        {
            var keySet = entry.Metadata.FindPrimaryKey();
            if (keySet != null)
                return keySet
                  .Properties
                  .Select(p => entry.Property(p.Name).CurrentValue).ToList();

            return null;
        }
        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public IRepository<T> GetRepository<T>() where T : class, IObjectState
        {
            return _unitOfWork.Repository<T>();
        }

        public virtual async Task<TEntity?> FindAsync<TType>(TType keyValue)
        {

            return await _dbSet.FindAsync(keyValue);
        }


        public virtual async Task<bool> DeleteAsync<TType>(TType keyValue)
        {
            var entity = await FindAsync(keyValue);

            if (entity == null)
            {
                return false;
            }

            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);

            return true;
        }



        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null,
            int? count = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            else if (count != null)
            {
                query.Take(count.Value);
            }

            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }

        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            _entitesChecked = null;
            _dbSet.Attach(entity);
        }

        HashSet<object> _entitesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph

        private void SyncObjectGraph(object entity) // scan object graph for all 
        {
            if (_entitesChecked == null)
                _entitesChecked = new HashSet<object>();

            if (_entitesChecked.Contains(entity))
                return;

            _entitesChecked.Add(entity);

            var objectState = entity as IObjectState;

            if (objectState != null && objectState.ObjectState == ObjectState.Added)
                _context.SyncObjectState((IObjectState)entity);

            // Set tracking state for child collections
            foreach (var prop in entity.GetType().GetProperties())
            {
                // Apply changes to 1-1 and M-1 properties
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if (trackableRef.ObjectState == ObjectState.Added)
                        _context.SyncObjectState((IObjectState)entity);

                    SyncObjectGraph(prop.GetValue(entity, null));
                }

                // Apply changes to 1-M properties
                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null) continue;

                Debug.WriteLine("Checking collection: " + prop.Name);

                foreach (var item in items)
                    SyncObjectGraph(item);
            }
        }

        //public async Task<TEntity> InsertAsync(TEntity entity)
        //{
        //   var res = await _dbSet.AddAsync(entity);

        //    //entity.ObjectState = ObjectState.Added;
        //    //_dbSet.Attach(entity);
        //    //_context.SyncObjectState(entity); 
        //    return entity;
        //}

        //public Task DeleteAsync(TEntity entity)
        //{
        //    _dbSet.Remove(entity);
        //    return Task.CompletedTask;
        //}

        //public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        //{
        //    var entities = _dbSet.Where(filter);

        //    _dbSet.RemoveRange(entities);

        //    return Task.CompletedTask;
        //}

        //public async Task<IEnumerable<TEntity>> GetAllAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}

        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        //public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? top = null, int? skip = null, params string[] includeProperties)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (includeProperties.Length > 0)
        //    {
        //        query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
        //    }

        //    if (orderBy != null)
        //    {
        //        query = orderBy(query);
        //    }

        //    if (skip.HasValue)
        //    {
        //        query = query.Skip(skip.Value);
        //    }

        //    if (top.HasValue)
        //    {
        //        query = query.Take(top.Value);
        //    }

        //    return await query.ToListAsync();
        //}
    }
}