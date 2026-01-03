using AutoMapper;
using MongoDB.Bson;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Data.Model;

namespace Restaurant.Identification.Presenter.Mappers;

public class ClientProfile : Profile
{

    public ClientProfile()
    {
        CreateMap<ClientData, ClientDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.ToString()))
            .ReverseMap()
            .ForMember(d => d.Id, o => o.MapFrom(s => new ObjectId(s.Id)));
    }

}
