namespace Service.Data.Entities;

public class VehicleMake
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }
    public virtual ICollection<VehicleModel> Models { get; set; }
}