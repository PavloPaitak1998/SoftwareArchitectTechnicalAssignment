using Microsoft.EntityFrameworkCore;
using Test.WebApplication.Dal.Entities;
using Test.WebApplication.Dal.EntityTypeConfigurations;

namespace Test.WebApplication.Dal.Db
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionStatusConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
