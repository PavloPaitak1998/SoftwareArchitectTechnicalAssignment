using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.WebApplication.Dal.Entities;

namespace Test.WebApplication.Dal.EntityTypeConfigurations
{
    internal class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder.ToTable("TransactionStatus");

            builder.Property(e => e.TransactionStatusId)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder.Property(e => e.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(e => e.UnifiedFormat)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength();
        }
    }
}
