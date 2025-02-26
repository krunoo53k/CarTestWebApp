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

    public async Task CreateAsync(CreateVehicleMakeDto vehicleMakeDto)
    {
        var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeDto);
        await _dbContext.VehicleMakes.AddAsync(vehicleMake);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateVehicleMakeDto updateVehicleMakeDto)
    {
        var vehicleMake = await _dbContext.VehicleMakes.FindAsync(updateVehicleMakeDto.Id);
        if (vehicleMake == null)
        {
            throw new Exception("VehicleMake not found");
        }

        string oldAbbrv = vehicleMake.Abrv;
        _mapper.Map(updateVehicleMakeDto, vehicleMake);
        
        
        // If the abbreviation has changed, so should the VehicleModel table be updated,
        // since the data is not normalized.
        if (!string.Equals(oldAbbrv, vehicleMake.Abrv, StringComparison.Ordinal))
        {
            var vehicleModels = _dbContext.VehicleModels.Where(vm => vm.MakeId == vehicleMake.Id);
            await vehicleModels.ForEachAsync(vm => vm.Abrv = vehicleMake.Abrv);
        }
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