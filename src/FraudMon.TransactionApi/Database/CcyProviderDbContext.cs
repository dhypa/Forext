using Forext.CcyProvider.Domain;
using Microsoft.EntityFrameworkCore;

namespace Forext.CcyProvider.Database;

public class CcyProviderDbContext : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyPair> CurrencyPairs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Currency>(b =>
        {
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.Code).IsRequired();
        });

        modelBuilder.Entity<CurrencyPair>(b =>
        {
            b.HasIndex(x => x.Symbol).IsUnique();
            b.Property(x => x.Symbol).IsRequired();

            b.HasOne(x => x.QuoteCurrency)
                .WithMany()
                .HasForeignKey(x => x.BaseCurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.BaseCurrency)
                .WithMany()
                .HasForeignKey(x => x.QuoteCurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            // CurrencyPair has 1 base Currency
            // Currency has many CurrencyPairs

            b.HasIndex(x => new { x.BaseCurrencyId, x.QuoteCurrencyId })
                .IsUnique();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

}