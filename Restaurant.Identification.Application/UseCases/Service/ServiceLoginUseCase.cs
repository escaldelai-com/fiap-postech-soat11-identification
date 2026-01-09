using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Presenter;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Application.Interfaces.UseCases;
using Restaurant.Identification.Model;

namespace Restaurant.Identification.Application.UseCases;

public class ServiceLoginUseCase(
    IServiceRepository repo,
    IHashPresenter presenter) : IServiceLoginUseCase
{

    public async Task<ServiceDto?> Login(ServiceLoginDto loginDto)
    {
        Validator.Create()
            .IsNotNull(loginDto)
            .Test(loginDto?.grant_type == "client_credentials", "invalid grant_type")
            .Validate();

        var service = await repo.Get(loginDto!.client_id!);

        if (service == null)
            return null;

        var secret = presenter.GetHash(loginDto!.client_secret!);

        if (service.Secret != secret)
            return null;

        return service;
    }

}
