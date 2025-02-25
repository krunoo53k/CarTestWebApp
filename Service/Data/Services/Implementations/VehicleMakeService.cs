using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Data.DTOs;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace Service.Data.Services.Implementations;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public VehicleMakeService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<VehicleMakeDto?> GetByIdAsync(int id)
    {
        var vehicleMake = await _dbContext.VehicleMakes.FindAsync(id);
        if (vehicleMake != null)
        {
            return _mapper.Map<VehicleMakeDto>(vehicleMake);
        }

        return null;
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