using System.ComponentModel.DataAnnotations;

namespace Service.Data.DTOs.VehicleModel;

public class CreateVehicleModelDto
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Make ID is required")]
    public int? MakeId { get; set; }
}