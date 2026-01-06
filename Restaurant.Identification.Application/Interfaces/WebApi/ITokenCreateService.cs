using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.WebApi;

public interface ITokenCreateService
{

    TokenDto Create(ServiceDto data);

}
