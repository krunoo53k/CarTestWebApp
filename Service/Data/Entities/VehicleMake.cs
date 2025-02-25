namespace Service.Data.Entities;

public class VehicleMake
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Abrv { get; set; }
    public virtual ICollection<VehicleModel> Models { get; set; }
    
    public VehicleMake()
    {
        Models = new List<VehicleModel>();
    }
}