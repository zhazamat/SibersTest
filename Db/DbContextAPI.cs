
using Microsoft.EntityFrameworkCore;

using SibersTest.Models;

namespace Onlineshop.Db
{
    public class DbContextAPI : DbContext
    {
        public DbContextAPI(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SibersTest.Models.Task>()
                .HasOne<EmployeeProject>(e=>e.Executor)
                .WithMany(t => t.Tasks)
                .HasForeignKey(m => m.ExecutorId);

            modelBuilder.Entity<EmployeeProject>()
               .HasOne<Employee>(e => e.Employee)
               .WithMany(ep=>ep.EmployeeProjects)
               .HasForeignKey(x=>x.EmployeeId);
        }
        public DbSet<Project> Projects { get; set; }
       
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SibersTest.Models.Task> Tasks { get; set; }
    }
}
