using Microsoft.Extensions.Configuration;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Facade;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Application.Interfaces.UseCases;

namespace Restaurant.Identification.Facade;

public class ClientFacade(
    IClientGetByCpfUseCase getByCpfUseCase,
    IClientRepository repo,
    IConfiguration configuration,
    IClientRegisterUseCase registerUseCase) : IClientFacade
{

    /// <summary>
    /// CMD: Identifica
    /// </summary>
    /// <returns>
    /// EV: Cliente identificado
    /// </returns>
    public async Task<ClientDto?> GetClientIdentify(string cpf)
    {
        return await getByCpfUseCase.GetClientIdentify(cpf);
    }

    /// <summary>
    /// CMD: Gera Lista de Clientes
    /// </summary>
    /// <returns>
    /// EV: Lista de clientes gerada
    /// </returns>
    public async Task<IEnumerable<ClientDto>> GetClientList()
    {
        return await repo.GetList();
    }

    /// <summary>
    /// CMD: Não Identifica
    /// </summary>
    /// <returns>
    /// EV: Cliente não identificado
    /// </returns>
    public async Task<ClientDto?> GetClientNoIdentify()
    {
        var id = configuration["Client:noIdentifyId"];

        if (string.IsNullOrEmpty(id))
            return null;

        return await repo.GetById(id);
    }

    /// <summary>
    /// CMD: Efetua Cadastro
    /// </summary>
    /// <returns>
    /// EV: Cliente cadastrado
    /// </returns>
    public async Task<string> Register(ClientDto client)
    {
        return await registerUseCase.Register(client);
    }

}
