using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Repository;

public interface IClientRepository
{

    Task<ClientDto?> GetById(string id);

    Task<ClientDto?> GetByCpf(string cpf);

    Task<IEnumerable<ClientDto>> GetList();

    Task<string> Save(ClientDto client);

}
