using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Cache;
using Restaurant.Identification.Application.Interfaces.Presenter;
using StackExchange.Redis;

namespace Restaurant.Identification.Data.Cache;

public class ServiceCache(
    IJsonPresenter presenter,
    IDatabase context) : IServiceCache
{

    public async Task<ServiceDto?> GetById(string? serviceId)
    {
        if (string.IsNullOrEmpty(serviceId))
            return null;

        var data = await context.ExecuteAsync("JSON.GET", GetKey(serviceId));

        return !data.IsNull
            ? presenter.Deserialize<ServiceDto>(data.ToString())
            : null;
    }

    public async Task SetService(ServiceDto service)
    {
        if (string.IsNullOrEmpty(service.Id))
            return;

        var data = presenter.Serialize(service);
        var key = GetKey(service.Id!);

        await context.ExecuteAsync("JSON.SET", key, "$", data!);
    }


    private static string GetKey(string? id)
    {
        return $"service:{id}";
    }
}
