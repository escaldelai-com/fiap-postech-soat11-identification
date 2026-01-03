using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Application.Interfaces.UseCases;
using Restaurant.Identification.Model;

namespace Restaurant.Identification.Application.UseCases;

public class ClientRegisterUseCase(
    IClientGetByCpfUseCase getByCpfUseCase,
    IClientRepository repo) : IClientRegisterUseCase
{

    public async Task<string> Register(ClientDto client)
    {
        Validator.Create()
            .IsNotNull(client)
            .Validate();

        var exists = await getByCpfUseCase.GetClientIdentify(client.CPF!);

        if (exists != null)
            return exists.Id!;

        var model = new Client(client.Nome!, client.CPF!, client.Email!);

        client.CPF = model.CPF.Numero;

        return await repo.Save(client);
    }

}
