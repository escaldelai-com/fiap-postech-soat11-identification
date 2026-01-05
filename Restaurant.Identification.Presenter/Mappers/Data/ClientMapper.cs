using AutoMapper;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Data.Model;

namespace Restaurant.Identification.Presenter.Mappers;

public class ClientMapper : Profile
{

    public ClientMapper()
    {
        CreateMap<ClientData, ClientDto>()
            .ReverseMap();
    }

}
