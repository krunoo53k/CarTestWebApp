using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarTestWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Data.DTOs;
using Service.Data.DTOs.VehicleMake;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace CarTestWebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleMakeController : Controller
{
    private readonly IVehicleMakeService _vehicleMakeService;
    private readonly IMapper _mapper;

    public VehicleMakeController(IVehicleMakeService vehicleMakeService,  IMapper mapper)
    {
        _vehicleMakeService = vehicleMakeService;
        _mapper = mapper;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute, Range(0, int.MaxValue, ErrorMessage = "ID must be between 0 and the maximum integer value.")] int id, CancellationToken cancellationToken)
    {
        var vehicleMake = await _vehicleMakeService.GetByIdAsync(id, cancellationToken);
        if (vehicleMake is null)
        {
            return NotFound();
        }
        
        var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
        
        return Ok(vehicleMakeViewModel);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken, [FromQuery] string sortOrder, [FromQuery] string? searchTerm, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var vehicleMakes = await _vehicleMakeService.GetAllAsync(cancellationToken, "name", sortOrder, searchTerm, pageNumber, pageSize);
        
        var vehicleMakesViewModel = _mapper.Map<IEnumerable<VehicleMakeViewModel>>(vehicleMakes);
        return Ok(vehicleMakesViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateVehicleMakeDto createVehicleMakeDto)
    {
        var createdVehicleMake = await _vehicleMakeService.CreateAsync(createVehicleMakeDto);

        if (createdVehicleMake is null)
        {
            return BadRequest(createVehicleMakeDto);
        }
        
        return CreatedAtAction(nameof(Get), new { id = createdVehicleMake.Id }, createdVehicleMake);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateVehicleMakeDto updateVehicleMakeDto)
    {
        try
        {
            await _vehicleMakeService.UpdateAsync(updateVehicleMakeDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute, Range(0, int.MaxValue, ErrorMessage = "ID must be between 0 and the maximum integer value.")] int id)
    {
        try
        {
            await _vehicleMakeService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}