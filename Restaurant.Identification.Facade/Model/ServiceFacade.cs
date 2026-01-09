using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Facade;
using Restaurant.Identification.Application.Interfaces.UseCases;
using Restaurant.Identification.Application.Interfaces.WebApi;
using Restaurant.Identification.Model;

namespace Restaurant.Identification.Facade;

public class ServiceFacade(
    ITokenCreateService tokenCreate,
    IServiceLoginUseCase useCase) : IServiceFacade
{

    public async Task<TokenDto> Login(ServiceLoginDto login)
    {
        var data = await useCase.Login(login);

        if (data == null)
            throw new NotAuthorizedException();

        return tokenCreate.Create(data);
    }

}
