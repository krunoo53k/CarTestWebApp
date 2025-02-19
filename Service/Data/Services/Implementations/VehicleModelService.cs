using Microsoft.EntityFrameworkCore;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace Service.Data.Services.Implementations;

public class VehicleModelService : IVehicleModelService
{
    private readonly ApplicationDbContext _dbContext;

    public VehicleModelService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<VehicleModel>> GetAllAsync()
    {
        return await _dbContext.VehicleModels.ToListAsync();
    }

    public async Task<VehicleModel> GetByIdAsync(int id)
    {
        return await _dbContext.VehicleModels.FindAsync(id);
    }

    public async Task CreateAsync(VehicleModel vehicleModel)
    {
        await _dbContext.VehicleModels.AddAsync(vehicleModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleModel vehicleModel)
    {
        _dbContext.VehicleModels.Update(vehicleModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleModel = await _dbContext.VehicleModels.FindAsync(id);
        if (vehicleModel != null)
        {
            _dbContext.VehicleModels.Remove(vehicleModel);
            await _dbContext.SaveChangesAsync();
        }
    }
}