using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Facade;

public interface IClientFacade
{

    Task<ClientDto?> GetClientNoIdentify();

    Task<IEnumerable<ClientDto>> GetClientList(IEnumerable<string> id);

    Task<ClientDto?> GetClientIdentify(string cpf);

    Task<ClientDto?> GetById(string id);

    Task<string> Register(ClientDto client);

}
