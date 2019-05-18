using ITI.Enterprise.InterviewTask.DataModel.EntityConfigurations;
using ITI.Enterprise.InterviewTask.DataModel.Extensions;
using ITI.Enterprise.InterviewTask.DomainClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITI.Enterprise.InterviewTask.DataModel
{
    public class AppDbContext : IdentityDbContext<User>
    {
       // private string _connectionString;


       public AppDbContext(DbContextOptions options) :base(options)
       {
           
       }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(
                "Server=.;Database=InterviewTask;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
    }
}
