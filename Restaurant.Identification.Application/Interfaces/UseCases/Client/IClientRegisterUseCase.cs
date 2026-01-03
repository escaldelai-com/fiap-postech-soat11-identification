using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.UseCases;

public interface IClientRegisterUseCase
{

    Task<string> Register(ClientDto client);

}
