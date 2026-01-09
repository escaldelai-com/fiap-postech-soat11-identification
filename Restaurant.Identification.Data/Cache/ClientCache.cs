using Microsoft.Extensions.Configuration;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Cache;
using Restaurant.Identification.Application.Interfaces.Presenter;
using StackExchange.Redis;

namespace Restaurant.Identification.Data.Cache;

public class ClientCache(
    IConfiguration configuration,
    IJsonPresenter presenter,
    IDatabase context) : IClientCache
{

    private readonly TimeSpan expiration =
        TimeSpan.Parse(configuration["Client:CacheExpiration"] ?? "02:00:00");


    public async Task<ClientDto?> GetByCpf(string cpf)
    {
        var data = await context.ExecuteAsync("JSON.GET", GetKey(cpf));

        return !data.IsNull
            ? presenter.Deserialize<ClientDto>(data.ToString())
            : null;
    }

    public async Task<ClientDto?> GetById(string id)
    {
        var data = await context.ExecuteAsync("JSON.GET", GetKey(id));

        return !data.IsNull
            ? presenter.Deserialize<ClientDto>(data.ToString())
            : null;
    }

    public async Task Set(ClientDto? client)
    {
        if (client == null)
            return;

        var data = presenter.Serialize(client);
        var batch = context.CreateBatch();
        var tasks = new List<Task>();

        foreach (var key in new[] { client.Id, client.CPF })
        {
            if (string.IsNullOrEmpty(key))
                continue;

            tasks.Add(batch.ExecuteAsync("JSON.SET", GetKey(key), "$", data!));
            tasks.Add(batch.KeyExpireAsync(GetKey(key), expiration));
        }

        batch.Execute();
        await Task.WhenAll(tasks);
    }




    private string GetKey(string id) => $"client:{id}";

}
