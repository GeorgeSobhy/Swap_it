 
using Microsoft.EntityFrameworkCore;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
 
namespace Repository.Pattern.EF
{
    public class DataContext : DbContext, IDataContextAsync
    {
        #region Private Fields
        private readonly Guid _instanceId;
        bool _disposed;
        #endregion Private Fields

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            _instanceId = Guid.NewGuid();
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //new AnyMap(modelBuilder.Entity<T>()); 
        }

 
        public Guid InstanceId { get { return _instanceId; } }
         
        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }


        public void RejectChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
         
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        private void SyncObjectsStatePreCommit()
        {
             
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }


        public override ValueTask DisposeAsync()
        {
            if (!_disposed)
            { 
                _disposed = true;
            }
            return base.DisposeAsync();
        }
        public override void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
            }
            base.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChangesAsync();
            SyncObjectsStatePostCommit();
            return changes;
        }
    }
}