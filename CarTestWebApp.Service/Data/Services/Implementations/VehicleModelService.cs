using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Data.DTOs;
using Service.Data.DTOs.VehicleModel;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace Service.Data.Services.Implementations;

public class VehicleModelService : BaseService<VehicleModel, VehicleModelDto, CreateVehicleModelDto, UpdateVehicleModelDto>,IVehicleModelService
{
    public VehicleModelService(ApplicationDbContext dbContext,  IMapper mapper) : base(dbContext, mapper)
    {
    }
    
    public override async Task<IEnumerable<VehicleModelDto>> GetAllAsync(CancellationToken cancellationToken, string  sortBy, string sortOrder, string? searchTerm, int pageNumber = 1, int pageSize = 10)
    {
        IQueryable<VehicleModel> query = _dbSet.Include(e => e.Make);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, $"{searchTerm}%"));
        }

        query = ApplySort(query, sortBy, sortOrder);
        
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        
        var vehicleModels = await query.ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<VehicleModelDto>>(vehicleModels);
    }
    
    public override async Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var vehicleModel = await _dbContext.VehicleModels
            .AsNoTracking()
            .Include(m => m.Make)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        
        var vehicleModelDto = _mapper.Map<VehicleModelDto>(vehicleModel);
        return vehicleModelDto;
    }

    public override async Task<VehicleModelDto?> CreateAsync(CreateVehicleModelDto createVehicleModelDto)
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
    
    protected override IQueryable<VehicleModel> ApplySort(IQueryable<VehicleModel> query, string? sortBy, string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            sortBy = "name";
        }
        
        if (sortBy.ToLower() is "make")
        {
            if (sortOrder is "desc")
                return query.OrderByDescending(e => e.Make.Name);
            return query.OrderBy(e => e.Make.Name);
        }

        if (sortOrder is "desc")
            return query.OrderByDescending(e => e.Name);
        return query.OrderBy(e => e.Name);
    }
}