using Fridges.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fridges.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<FridgeModel> FridgeModels { get; set; }
    public DbSet<FridgeProduct> FridgeProducts { get; set; }
    //int SaveChanges();
    //Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
