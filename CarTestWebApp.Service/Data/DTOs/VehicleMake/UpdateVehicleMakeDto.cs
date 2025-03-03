using System.ComponentModel.DataAnnotations;
using Service.Data.DTOs.Generics;

namespace Service.Data.DTOs.VehicleMake;

public class UpdateVehicleMakeDto : IUpdateDto
{
    [Required(ErrorMessage = "ID is required")]
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Abrv { get; set; }
}