using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Cache;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Data.Model;

namespace Restaurant.Identification.Data.Repositories;

public class ClientRepository(
    IMapper mapper,
    IClientCache cache,
    IMongoDatabase context) : IClientRepository
{

    private readonly IMongoCollection<ClientData> collection = 
        context.GetCollection<ClientData>("client");


    public async Task<ClientDto?> GetByCpf(string cpf)
    {
        var dto = await cache.GetByCpf(cpf);

        if (dto != null)
            return dto;

        var data = await collection
            .Find(c => c.CPF == cpf)
            .FirstOrDefaultAsync();

        dto = mapper.Map<ClientDto>(data);
        await cache.Set(dto);

        return dto;
    }

    public async Task<ClientDto?> GetById(string id)
    {
        var dto = await cache.GetById(id);

        if (dto != null)
            return dto;

        var data = await collection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        dto = mapper.Map<ClientDto>(data);
        await cache.Set(dto);

        return dto;
    }

    public async Task<IEnumerable<ClientDto>> GetList()
    {
        var data = await collection
            .Find(_ => true)
            .ToListAsync();

        return mapper.Map<IEnumerable<ClientDto>>(data);
    }

    public async Task<IEnumerable<ClientDto>> GetList(IEnumerable<string> ids)
    {
        var filter = Builders<ClientData>.Filter
            .In(x => x.Id, ids);

        var data = await collection
            .Find(filter)
            .ToListAsync();

        return mapper.Map<IEnumerable<ClientDto>>(data);
    }

    public async Task<string> Save(ClientDto client)
    {
        var data = mapper.Map<ClientData>(client);

        await collection.InsertOneAsync(data);

        return data.Id!.ToString();
    }

}
