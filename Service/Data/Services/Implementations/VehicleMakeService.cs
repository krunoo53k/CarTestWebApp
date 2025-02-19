using Microsoft.EntityFrameworkCore;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace Service.Data.Services.Implementations;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly ApplicationDbContext _dbContext;

    public VehicleMakeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VehicleMake> GetByIdAsync(int id)
    {
        return await _dbContext.VehicleMakes.FindAsync(id);
    }

    public async Task<IEnumerable<VehicleMake>> GetAllAsync()
    {
        return await _dbContext.VehicleMakes.ToListAsync();
    }

    public async Task CreateAsync(VehicleMake vehicleMake)
    {
        await _dbContext.VehicleMakes.AddAsync(vehicleMake);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleMake vehicleMake)
    {
        _dbContext.Update(vehicleMake);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleMake = await _dbContext.VehicleMakes.FindAsync(id);
        if (vehicleMake != null)
        {
            _dbContext.VehicleMakes.Remove(vehicleMake);
            await _dbContext.SaveChangesAsync();
        }
    }
}