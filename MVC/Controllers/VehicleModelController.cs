using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Data;
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
        return Ok(vehicleModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel([FromBody] CreateVehicleModelDto createVehicleModelDto)
    {
        await _vehicleModelService.CreateAsync(createVehicleModelDto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVehicleModel([FromBody] VehicleModel vehicleModel)
    {
        await _vehicleModelService.UpdateAsync(vehicleModel);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        await _vehicleModelService.DeleteAsync(id);
        return Ok();
    }
}