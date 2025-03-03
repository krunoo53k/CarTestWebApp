using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Data.DTOs;
using Service.Data.DTOs.VehicleModel;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace Service.Data.Services.Implementations;

public class VehicleModelService : IVehicleModelService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public VehicleModelService(ApplicationDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<VehicleModelDto>> GetAllAsync(string  sortBy, string sortOrder, string? searchTerm, CancellationToken cancellationToken , int pageNumber = 1, int pageSize = 10)
    {
        IQueryable<VehicleModel> query = _dbContext.VehicleModels.Include(m => m.Make);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(m => EF.Functions.Like(m.Name, $"{searchTerm}%"));
        }

        switch (sortBy)
        {
            case "make":
                query = sortOrder == "asc"
                    ? query.OrderBy(m => m.Make.Name)
                    : query.OrderByDescending(m => m.Make.Name);
                break;
            case "model":
                default:
                query = sortOrder == "asc"
                ? query.OrderBy(m => m.Name)
                : query.OrderByDescending(m => m.Name);
                break;
        }
        
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        
        var vehicleModels = await query.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<VehicleModelDto>>(vehicleModels);
    }

    public async Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var vehicleModel = await _dbContext.VehicleModels
            .AsNoTracking()
            .Include(m => m.Make)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        
        var vehicleModelDto = _mapper.Map<VehicleModelDto>(vehicleModel);
        return vehicleModelDto;
    }

    public async Task<VehicleModelDto?> CreateAsync(CreateVehicleModelDto createVehicleModelDto)
    {
        var vehicleModel = _mapper.Map<VehicleModel>(createVehicleModelDto);
        
        var make = await _dbContext.VehicleMakes
            .FirstOrDefaultAsync(m => m.Id == createVehicleModelDto.MakeId);
    
        if (make == null)
        {
            return null;
        }
        
        vehicleModel.Make = make;
        vehicleModel.Abrv = make.Abrv;
        
        await _dbContext.VehicleModels.AddAsync(vehicleModel);
        await _dbContext.SaveChangesAsync();
        
        return  _mapper.Map<VehicleModelDto>(vehicleModel);
    }

    public async Task UpdateAsync(UpdateVehicleModelDto updateVehicleModelDto)
    {
        var vehicleModel = await _dbContext.VehicleModels.FindAsync(updateVehicleModelDto.Id);
        
        if (vehicleModel == null)
        {
            throw new KeyNotFoundException($"VehicleModel not found for the provided Id ({updateVehicleModelDto.Id}).");
        }
        
        _mapper.Map(updateVehicleModelDto, vehicleModel);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleModel = await _dbContext.VehicleModels.FindAsync(id);
        if (vehicleModel != null)
        {
            _dbContext.VehicleModels.Remove(vehicleModel);
            await _dbContext.SaveChangesAsync();
        }
    }
}