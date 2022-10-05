using System;
using Microsoft.EntityFrameworkCore;

using LPC.Contracts.Database;

namespace LPC.Domain.Database;

public class LpcDbContext : DbContext
{
    public DbSet<Record> Records { get; init; }
    public DbSet<Library> Libraries { get; init; }

    public DbSet<Wishlist> Wishlists { get; init; }
    public LpcDbContext() : base()
    {
    }

    public LpcDbContext(DbContextOptions<LpcDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wishlist>().HasOne<Record>(x => x.Record);
        modelBuilder.Entity<Library>().HasOne<Record>(x => x.Record);
        base.OnModelCreating(modelBuilder);
    }
}