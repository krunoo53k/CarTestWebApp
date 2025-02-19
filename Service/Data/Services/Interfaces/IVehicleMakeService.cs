using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleMakeService
{
    Task<VehicleMake> GetByIdAsync(int id);
    Task<IEnumerable<VehicleMake>> GetAllAsync();
    Task CreateAsync(VehicleMake vehicleMake);
    Task UpdateAsync(VehicleMake vehicleMake);
    Task DeleteAsync(int id);
}