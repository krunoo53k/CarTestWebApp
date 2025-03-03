namespace Service.Data.DTOs;

namespace Service.Data.DTOs.VehicleModel;

public class UpdateVehicleModelDto : IUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? MakeId { get; set; }
}