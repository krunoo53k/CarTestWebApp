namespace CarTestWebApp.Models;

public class VehicleMakeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }
    public virtual IEnumerable<VehicleModelViewModel> Models { get; set; }
}