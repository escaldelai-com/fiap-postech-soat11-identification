using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Application.Interfaces.UseCases;
using Restaurant.Identification.Model;

namespace Restaurant.Identification.Application.UseCases;

public class ClientGetByCpfUseCase(
    IClientRepository repo) : IClientGetByCpfUseCase
{

    public async Task<ClientDto?> GetClientIdentify(string cpf)
    {
        var model = new CPF(cpf);

        return await repo.GetByCpf(model.Numero);
    }

}
