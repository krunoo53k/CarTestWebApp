using Service.Data.DTOs.Generics;

namespace Service.Data.DTOs.VehicleMake;

public class UpdateVehicleMakeDto : IUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Abrv { get; set; }
}