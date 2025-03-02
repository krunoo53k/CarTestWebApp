using Service.Data.DTOs;
using Service.Data.Entities;

namespace Service.Data.Services.Interfaces;

public interface IVehicleMakeService
{
    Task<VehicleMakeDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<VehicleMakeDto>> GetAllAsync(string sortOrder, string? searchTerm, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<VehicleMakeDto> CreateAsync(CreateVehicleMakeDto vehicleMakeDto);
    Task UpdateAsync(UpdateVehicleMakeDto updateVehicleMakeDto);
    Task DeleteAsync(int id);
}