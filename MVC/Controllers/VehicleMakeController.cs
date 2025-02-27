using Microsoft.AspNetCore.Mvc;
using Service.Data.DTOs;
using Service.Data.Entities;
using Service.Data.Services.Interfaces;

namespace CarTestWebApp.Controllers;

[Route("api/[controller]")]
public class VehicleMakeController : Controller
{
    private readonly IVehicleMakeService _vehicleMakeService;

    public VehicleMakeController(IVehicleMakeService vehicleMakeService)
    {
        _vehicleMakeService = vehicleMakeService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var vehicleMake = await _vehicleMakeService.GetByIdAsync(id);
        if (vehicleMake is null)
        {
            return NotFound();
        }
        return Ok(vehicleMake);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] string sortOrder)
    {
        var vehicleMakes = await _vehicleMakeService.GetAllAsync(sortOrder);
        return Ok(vehicleMakes);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateVehicleMakeDto createVehicleMakeDto)
    {
        await _vehicleMakeService.CreateAsync(createVehicleMakeDto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateVehicleMakeDto updateVehicleMakeDto)
    {
        await _vehicleMakeService.UpdateAsync(updateVehicleMakeDto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _vehicleMakeService.DeleteAsync(id);
        return Ok();
    }
}