using Service.Data.Entities;

namespace Service.Data.Repositories.Interfaces;

public interface IVehicleModelRepository
{
    Task<VehicleModel> GetByIdAsync(int id);
    Task<IEnumerable<VehicleModel>> GetAllAsync();
    Task<IEnumerable<VehicleModel>> GetByMakeIdAsync(int makeId);
    Task AddAsync(VehicleModel vehicleModel);
    Task UpdateAsync(VehicleModel vehicleModel);
    Task DeleteAsync(int id);
}
    