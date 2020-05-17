using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.WebApplication.Dal.Entities;

namespace Test.WebApplication.Dal.EntityTypeConfigurations
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.ModifiedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getutcdate())");

            builder.Property(e => e.TransactionDate).HasColumnType("datetime");

            builder.Property(e => e.TransactionIdentificator)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.TransactionIdentificator)
                .IsUnique();

            builder.Property(e => e.CurrencyCodeId)
                .HasConversion<int>();

            builder.Property(e => e.TransactionStatusId)
                .HasConversion<int>();

            builder.HasOne(d => d.CurrencyCode)
                .WithMany()
                .HasForeignKey(d => d.CurrencyCodeId);

            builder.HasOne(d => d.TransactionStatus)
                .WithMany()
                .HasForeignKey(d => d.TransactionStatusId);
        }
    }
}
