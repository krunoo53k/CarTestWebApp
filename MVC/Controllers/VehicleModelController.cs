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
        // TODO: Get VehicleModelDto instead of VehicleModel 
        var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
        if (vehicleModel == null)
        {
            return NotFound();
        }
        
        var vehicleModelDto = _mapper.Map<VehicleModelDto>(vehicleModel);
        var viewModel = _mapper.Map<VehicleModelViewModel>(vehicleModelDto);
        
        return Ok(viewModel);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] string sortBy = "model",  [FromQuery] string sortOrder = "asc")
    {
        var vehicleModels = await _vehicleModelService.GetAllAsync(sortBy, sortOrder);
        var vehicleModelViewModels = _mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicleModels);
        return Ok(vehicleModelViewModels);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel([FromBody] CreateVehicleModelDto createVehicleModelDto)
    {
        await _vehicleModelService.CreateAsync(createVehicleModelDto);
        return Ok();
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