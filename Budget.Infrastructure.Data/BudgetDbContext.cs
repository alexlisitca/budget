using Budget.Domain.Core.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Budget.Infrastructure.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext() : base("Budget-Database") { }

        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
