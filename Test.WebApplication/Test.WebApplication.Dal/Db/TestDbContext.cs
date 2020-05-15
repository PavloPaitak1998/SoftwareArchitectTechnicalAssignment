using Microsoft.EntityFrameworkCore;
using Test.WebApplication.Dal.Entities;

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

        public DbSet<CurrencyCode> CurrencyCodes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyCode>(entity =>
            {
                entity.ToTable("CurrencyCode");

                entity.Property(e => e.CurrencyCodeId).ValueGeneratedNever();

                entity.Property(e => e.CurrencyCodeValue)
                    .IsRequired()
                    .HasColumnName("CurrencyCode")
                    .HasMaxLength(3)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionIdentificator)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CurrencyCode)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyCodeId);

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany()
                    .HasForeignKey(d => d.TransactionStatusId);
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.ToTable("TransactionStatus");

                entity.Property(e => e.TransactionStatusId).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.UnifiedFormat)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength();
            });
        }
    }
}
