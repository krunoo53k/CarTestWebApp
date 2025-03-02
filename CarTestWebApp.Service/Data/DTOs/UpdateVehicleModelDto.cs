namespace Service.Data.DTOs;

public class UpdateVehicleModelDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? MakeId { get; set; }
}