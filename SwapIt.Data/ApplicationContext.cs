

using SwapIt.Data.Entities;
using SwapIt.Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Repository.Pattern.EF;
using Repository.Pattern.Infrastructure;
using RquestContext;
using RquestContext.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;


namespace SwapIt.Data
{
    public partial class ApplicationContext : DataContext
    {
        //public static ApplicationContext()
        //{
        //    Database.SetInitializer<DbContext>(null);

        // }
        IRequestContext _requestContext;
        ConfigurationValuesModel _configurationValues;
        public ApplicationContext(DbContextOptions<DataContext> options, IRequestContext requestContext, IOptions<ConfigurationValuesModel> configurationValuesOptions)
            : base(options)
        {
            _requestContext = requestContext;
            _configurationValues = configurationValuesOptions.Value;
        }

        public DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<City> Citys { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<CustomerBalance> CustomerBalances { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<ServiceBookmark> ServiceBookmarks { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceStatus> ServiceStatuss { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ErrorLog>().ToTable("ErrorLog");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<ServiceType>().ToTable("ServiceType");
            modelBuilder.Entity<Service>().ToTable("Service");


            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<CustomerBalance>().ToTable("CustomerBalance");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatus ");
            modelBuilder.Entity<ServiceBookmark>().ToTable("ServiceBookmark");
            modelBuilder.Entity<ServiceImage>().ToTable("ServiceImage");
            modelBuilder.Entity<ServiceRequest>().ToTable("ServiceRequest");
            modelBuilder.Entity<ServiceStatus>().ToTable("ServiceStatus");


            //add views
            new ViewsMap(modelBuilder);
            //add Procedures no key
            new ProceduresMap(modelBuilder);




        }
        public override int SaveChanges()
        {
            // CUSTOM LOGIC HERE IF NEEDED

            return base.SaveChanges();

        }

    }
}
