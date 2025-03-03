using Service.Data.DTOs;
using Service.Data.DTOs.VehicleModel;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleModelService
{
    Task<IEnumerable<VehicleModelDto>> GetAllAsync(CancellationToken cancellationToken, string  sortBy, string sortOrder, string? searchTerm, int pageNumber, int pageSize);
    Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<VehicleModelDto?> CreateAsync(CreateVehicleModelDto createVehicleModelDto);
    Task UpdateAsync(UpdateVehicleModelDto updateVehicleModelDto);
    Task DeleteAsync(int id);
}