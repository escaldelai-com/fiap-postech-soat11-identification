using AutoMapper;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Data.Model;


namespace Restaurant.Identification.Presenter.Mappers.Data;

public class ServiceMapper : Profile
{

    public ServiceMapper()
    {
        CreateMap<ServiceData, ServiceDto>()
            .ReverseMap();
    }

}
