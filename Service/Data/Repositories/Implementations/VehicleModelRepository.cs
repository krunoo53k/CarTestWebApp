using Microsoft.EntityFrameworkCore;
using Service.Data.Entities;
using Service.Data.Repositories.Interfaces;

namespace Service.Data.Repositories.Implementations;

public class VehicleModelRepository : IVehicleModelRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VehicleModelRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<VehicleModel> GetByIdAsync(int id)
    {
        return await _dbContext.VehicleModels.FindAsync(id);
    }

    public async Task<IEnumerable<VehicleModel>> GetAllAsync()
    {
        return await _dbContext.VehicleModels.ToListAsync();
    }

    public async Task<IEnumerable<VehicleModel>> GetByMakeIdAsync(int makeId)
    {
        return await _dbContext.VehicleModels.Where(x => x.MakeId == makeId).ToListAsync();
    }

    public async Task AddAsync(VehicleModel vehicleModel)
    {
        await  _dbContext.VehicleModels.AddAsync(vehicleModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleModel vehicleModel)
    {
        _dbContext.Update(vehicleModel);
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