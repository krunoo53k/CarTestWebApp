using Service.Data.DTOs;
using Service.Data.DTOs.VehicleMake;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleMakeService
{
    Task<VehicleMakeDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<VehicleMakeDto>> GetAllAsync(CancellationToken cancellationToken, string sortBy, string sortOrder, string? searchTerm, int pageNumber, int pageSize);
    Task<VehicleMakeDto?> CreateAsync(CreateVehicleMakeDto vehicleMakeDto);
    Task UpdateAsync(UpdateVehicleMakeDto updateVehicleMakeDto);
    Task DeleteAsync(int id);
}