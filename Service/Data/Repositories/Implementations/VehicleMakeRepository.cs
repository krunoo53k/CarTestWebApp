using Microsoft.EntityFrameworkCore;
using Service.Data.Entities;
using Service.Data.Repositories.Interfaces;

namespace Service.Data.Repositories.Implementations;

public class VehicleMakeRepository  : IVehicleMakeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VehicleMakeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VehicleMake> GetVehicleMakeByIdAsync(int id)
    {
        return await _dbContext.VehicleMakes.FindAsync(id);
    }

    public async Task<IEnumerable<VehicleMake>> GetAllAsync()
    {
        return await _dbContext.VehicleMakes.ToListAsync();
    }

    public async Task AddAsync(VehicleMake vehicleMake)
    {
        await _dbContext.VehicleMakes.AddAsync(vehicleMake);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleMake vehicleMake)
    {
        _dbContext.VehicleMakes.Update(vehicleMake);
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