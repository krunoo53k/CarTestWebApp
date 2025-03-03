using Service.Data.DTOs.Generics;

namespace Service.Data.Entities;

public class VehicleModel : IBaseEntity
{
    public int Id { get; set; }
    public int MakeId { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }
    public virtual VehicleMake Make { get; set; }
}