using System;
using Microsoft.EntityFrameworkCore;

using LPC.Contracts.Database;

namespace LPC.Domain.Database;

public class LpcDbContext : DbContext
{
    public DbSet<Record> Records { get; init; }
    public DbSet<OwnedRecord> OwnedRecords { get; init; }

    public DbSet<Wishlist> Wishlists { get; init; }
    public LpcDbContext() : base()
    {
    }

    public LpcDbContext(DbContextOptions<LpcDbContext> options) : base(options)
    {
    }
}