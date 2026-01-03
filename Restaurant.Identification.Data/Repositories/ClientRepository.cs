using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Data.Model;

namespace Restaurant.Identification.Data.Repositories;

public class ClientRepository(
    IMapper mapper,
    IMongoDatabase context) : IClientRepository
{

    private readonly IMongoCollection<ClientData> collection = 
        context.GetCollection<ClientData>("client");


    public async Task<ClientDto?> GetByCpf(string cpf)
    {
        var data = await collection
            .Find(c => c.CPF == cpf)
            .FirstOrDefaultAsync();

        return mapper.Map<ClientDto>(data);
    }

    public async Task<ClientDto?> GetById(string id)
    {
        var data = await collection
            .Find(c => c.Id == new ObjectId(id))
            .FirstOrDefaultAsync();

        return mapper.Map<ClientDto>(data);
    }

    public async Task<IEnumerable<ClientDto>> GetList()
    {
        var data = await collection
            .Find(_ => true)
            .ToListAsync();

        return mapper.Map<IEnumerable<ClientDto>>(data);
    }

    public async Task<string> Save(ClientDto client)
    {
        var data = mapper.Map<ClientData>(client);

        await collection.InsertOneAsync(data);

        return data.Id.ToString();
    }

}
