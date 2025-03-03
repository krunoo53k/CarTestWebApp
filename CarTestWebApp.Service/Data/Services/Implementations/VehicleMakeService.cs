using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Data.DTOs;
using Service.Data.DTOs.VehicleMake;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace Service.Data.Services.Implementations;

public class VehicleMakeService : BaseService<VehicleMake, VehicleMakeDto, CreateVehicleMakeDto, UpdateVehicleMakeDto>, IVehicleMakeService
{
    public VehicleMakeService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
    
    public override async Task UpdateAsync(UpdateVehicleMakeDto updateVehicleMakeDto)
    {
        var vehicleMake = await _dbContext.VehicleMakes.FindAsync(updateVehicleMakeDto.Id);
        if (vehicleMake == null)
        {
            throw new KeyNotFoundException($"VehicleMake with provided id {updateVehicleMakeDto.Id} not found");
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
    
}