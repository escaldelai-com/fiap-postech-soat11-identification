using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.UseCases;

public interface IClientGetByCpfUseCase
{

    Task<ClientDto?> GetClientIdentify(string cpf);

}
