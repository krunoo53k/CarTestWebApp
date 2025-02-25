using Service.Data.DTOs;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleModelService
{
    Task<IEnumerable<VehicleModelDto>> GetAllAsync();
    Task<VehicleModel?> GetByIdAsync(int id);
    Task CreateAsync(CreateVehicleModelDto createVehicleModelDto);
    Task UpdateAsync(VehicleModel vehicleModel);
    Task DeleteAsync(int id);
}