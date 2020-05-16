using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.WebApplication.Dal.Entities;

namespace Test.WebApplication.Dal.EntityTypeConfigurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("CurrencyCode");

            builder.Property(e => e.CurrencyCodeId)
                .ValueGeneratedNever()
                .HasConversion<int>();

            builder.Property(e => e.CurrencyCode)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();
        }
    }
}
