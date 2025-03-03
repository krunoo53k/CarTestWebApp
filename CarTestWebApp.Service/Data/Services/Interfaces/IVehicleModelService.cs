using Service.Data.DTOs;
using Service.Data.DTOs.VehicleModel;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleModelService
{
    Task<IEnumerable<VehicleModelDto>> GetAllAsync(string  sortBy, string sortOrder, string? searchTerm, CancellationToken cancellationToken, int pageNumber, int pageSize);
    Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<VehicleModelDto?> CreateAsync(CreateVehicleModelDto createVehicleModelDto);
    Task UpdateAsync(UpdateVehicleModelDto updateVehicleModelDto);
    Task DeleteAsync(int id);
}