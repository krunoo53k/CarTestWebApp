using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Data.DTOs.Generics;

namespace Service.Data.Services.Implementations;

public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : class, IBaseEntity
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class, IUpdateDto
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly IMapper _mapper;

    public BaseService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public virtual async Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (entity is not null)
        {
            return _mapper.Map<TDto>(entity);
        }

        return null;
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken, string sortBy,
        string sortOrder, string? searchTerm, int pageNumber = 1, int pageSize = 10)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, $"{searchTerm}%"));
        }

        query = sortOrder == "desc" ? 
            query.OrderByDescending(e => e.Name) : 
            query.OrderBy(e => e.Name);

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var entities = await query.ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public virtual async Task<TDto?> CreateAsync(TCreateDto createDto)
    {
        var entity = _mapper.Map<TEntity>(createDto);
        _dbSet.Add(entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task UpdateAsync(TUpdateDto updateDto)
    {
        var entity = await _dbSet.FindAsync(updateDto.Id);
        if (entity is null)
            throw new KeyNotFoundException($"Entity with id {updateDto.Id} not found");
        _mapper.Map(updateDto, entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        throw new KeyNotFoundException($"Entity with id {id} not found");
    }

    protected virtual IQueryable<TEntity> ApplySort(IQueryable<TEntity> query, string? sortBy, string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            sortBy = "name";
        }

        if (sortBy.ToLower() == "name")
        {
            query = sortOrder?.ToLower() == "desc" ? 
                query.OrderByDescending(e => e.Name) : 
                query.OrderBy(e => e.Name);
        }

        return query;
    }
}