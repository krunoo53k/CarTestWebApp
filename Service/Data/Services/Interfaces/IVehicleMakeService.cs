using Service.Data.DTOs;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleMakeService
{
    Task<VehicleMakeDto?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleMake>> GetAllAsync();
    Task CreateAsync(CreateVehicleMakeDto vehicleMakeDto);
    Task UpdateAsync(UpdateVehicleMakeDto updateVehicleMakeDto);
    Task DeleteAsync(int id);
}