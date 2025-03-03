using System.ComponentModel.DataAnnotations;
using Service.Data.DTOs.Generics;

namespace Service.Data.DTOs.VehicleModel;

public class UpdateVehicleModelDto : IUpdateDto
{
    [Required(ErrorMessage = "ID is required")]
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? MakeId { get; set; }
}