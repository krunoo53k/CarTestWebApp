using System.ComponentModel.DataAnnotations;

namespace Service.Data.DTOs.VehicleMake;

public class CreateVehicleMakeDto
{
    [Required(ErrorMessage = "Make name is required")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Make abbreviation is required")]
    public required string Abrv { get; set; }
}