using Microsoft.EntityFrameworkCore;
using Service.Data.Entities;

namespace Service.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<VehicleMake> VehicleMakes { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<VehicleModel>()
            .HasOne(m => m.Make)
            .WithMany(m => m.Models)
            .HasForeignKey(m => m.MakeId);
    }
}