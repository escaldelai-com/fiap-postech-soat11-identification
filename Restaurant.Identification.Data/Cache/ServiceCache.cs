using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Cache;
using Restaurant.Identification.Application.Interfaces.Presenter;
using StackExchange.Redis;

namespace Restaurant.Identification.Data.Cache;

public class ServiceCache(
    IJsonPresenter presenter,
    IDatabase context) : IServiceCache
{

    public async Task<ServiceDto?> Get(string? serviceId)
    {
        if (string.IsNullOrEmpty(serviceId))
            return null;

        var data = await context.ExecuteAsync("JSON.GET", GetKey(serviceId));

        return !data.IsNull
            ? presenter.Deserialize<ServiceDto>(data.ToString())
            : null;
    }

    public async Task Set(ServiceDto service)
    {
        if (string.IsNullOrEmpty(service.Id))
            return;

        var data = presenter.Serialize(service);
        var key = GetKey(service.Id!);

        await context.ExecuteAsync("JSON.SET", key, "$", data!);
    }


    private string GetKey(string? id) => $"service:{id}";


}
