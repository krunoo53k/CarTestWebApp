using Service.Data.Entities;

namespace Service.Data.Repositories.Interfaces;

public interface IVehicleMakeRepository
{
    Task<VehicleMake> GetVehicleMakeByIdAsync(int id);
    Task<IEnumerable<VehicleMake>> GetAllAsync();
    Task AddAsync(VehicleMake vehicleMake);
    Task UpdateAsync(VehicleMake vehicleMake);
    Task DeleteAsync(int id);
}