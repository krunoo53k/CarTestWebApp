using AutoMapper;
using CarTestWebApp.Models;
using Service.Data.DTOs;
using Service.Data.DTOs.VehicleMake;
using Service.Data.DTOs.VehicleModel;
using Service.Data.Entities;

namespace CarTestWebApp.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleModel, VehicleModelDto>()
            // FIXME Make might be null. This will create runtime errors.
            .ForMember(dest => dest.MakeAbrv, opt => opt.MapFrom(src => src.Make.Abrv));
        CreateMap<VehicleMake, VehicleMakeDto>();

        CreateMap<VehicleMakeDto, VehicleMakeViewModel>();
        CreateMap<VehicleModelDto, VehicleModelViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name} ({src.MakeAbrv})"));
        
        CreateMap<CreateVehicleMakeDto, VehicleMake>();
        CreateMap<CreateVehicleModelDto, VehicleModel>();
        
        CreateMap<UpdateVehicleMakeDto, VehicleMake>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<UpdateVehicleModelDto, VehicleModel>()
            .ForMember(dest => dest.MakeId, opt => opt.PreCondition(src => src.MakeId.HasValue))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}