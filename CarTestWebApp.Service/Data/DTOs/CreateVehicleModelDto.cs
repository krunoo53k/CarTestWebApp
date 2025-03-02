namespace Service.Data.DTOs;

public class CreateVehicleModelDto
{
    public required string Name { get; set; }
    public required int MakeId { get; set; }
}