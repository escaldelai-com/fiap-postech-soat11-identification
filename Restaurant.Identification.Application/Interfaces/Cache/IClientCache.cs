using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Cache;

public interface IClientCache
{

    Task<ClientDto?> GetByCpf(string cpf);

    Task<ClientDto?> GetById(string id);

    Task Set(ClientDto? client);

}
