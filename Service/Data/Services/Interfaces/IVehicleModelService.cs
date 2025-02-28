using Service.Data.DTOs;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleModelService
{
    Task<IEnumerable<VehicleModelDto>> GetAllAsync(string  sortBy, string sortOrder, string? searchTerm, int pageNumber, int pageSize);
    Task<VehicleModelDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateVehicleModelDto createVehicleModelDto);
    Task UpdateAsync(UpdateVehicleModelDto updateVehicleModelDto);
    Task DeleteAsync(int id);
}