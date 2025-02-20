using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleModelService
{
    Task<IEnumerable<VehicleModel>> GetAllAsync();
    Task<VehicleModel?> GetByIdAsync(int id);
    Task CreateAsync(VehicleModel vehicleModel);
    Task UpdateAsync(VehicleModel vehicleModel);
    Task DeleteAsync(int id);
}