using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Facade;

public interface IClientFacade
{

    Task<ClientDto?> GetClientNoIdentify();

    Task<IEnumerable<ClientDto>> GetClientList();

    Task<ClientDto?> GetClientIdentify(string cpf);

    Task<string> Register(ClientDto client);

}
