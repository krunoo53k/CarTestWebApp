namespace Service.Data.DTOs.VehicleModel;

public class CreateVehicleModelDto
{
    public required string Name { get; set; }
    public required int MakeId { get; set; }
}