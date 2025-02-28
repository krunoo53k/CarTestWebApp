using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CarTestWebApp.Models;
using Service.Data.DTOs;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace CarTestWebApp.Controllers;

[Route("api/[controller]")]
public class VehicleModelController : Controller
{
    private readonly IVehicleModelService _vehicleModelService;
    private readonly IMapper _mapper;

    public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
    {
        _vehicleModelService = vehicleModelService;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVehicleModel(int id)
    {
        var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
        if (vehicleModel == null)
        {
            return NotFound();
        }
        
        var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
        
        return Ok(viewModel);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] string sortBy = "model",  [FromQuery] string sortOrder = "asc", [FromQuery] string? searchTerm = null, int pageNumber = 1, int pageSize = 10)
    {
        var vehicleModels = await _vehicleModelService.GetAllAsync(sortBy, sortOrder, searchTerm,  pageNumber, pageSize);
        var vehicleModelViewModels = _mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicleModels);
        return Ok(vehicleModelViewModels);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel([FromBody] CreateVehicleModelDto createVehicleModelDto)
    {
        var createdVehicleModel = await _vehicleModelService.CreateAsync(createVehicleModelDto);
        if (createdVehicleModel == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetVehicleModel), new { id = createdVehicleModel.Id }, createdVehicleModel);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVehicleModel([FromBody] UpdateVehicleModelDto updateVehicleModelDto)
    {
        await _vehicleModelService.UpdateAsync(updateVehicleModelDto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        await _vehicleModelService.DeleteAsync(id);
        return Ok();
    }
}