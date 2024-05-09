#region

using CommonServiceLocator;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
#endregion

namespace Repository.Pattern.EF
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        //  that was causing issue when try to fix Datacontext   IDataContextAsync when change it to DataCOntext ! because  services.AddScoped<IDataContextAsync, ApplicationContext>();
        private IDataContextAsync _dataContext;
        private bool _disposed;
        private IDbContextTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IDataContextAsync dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Dictionary<string, dynamic>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                { 
                    if (((DbContext)_dataContext).Database.GetDbConnection().State == ConnectionState.Open)
                    {
                        ((DbContext)_dataContext).Database.GetDbConnection().Close();
                    } 
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }

                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public void RejectChanges()
        {
            _dataContext.RejectChanges();

        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity>? RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            var _instance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this);
            if (_instance != null)
            {
                _repositories.Add(type, _instance);
                return _repositories[type];
            }
            return null;

        }

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        { 
            var _connection = ((DbContext)_dataContext).Database.GetDbConnection();
             
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            _transaction = ((DbContext)_dataContext).Database.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _dataContext.SyncObjectsStatePostCommit();
        }


        #endregion
    }
}