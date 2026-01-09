using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Facade;

public interface IServiceFacade
{

    Task<TokenDto> Login(ServiceLoginDto login);

}
