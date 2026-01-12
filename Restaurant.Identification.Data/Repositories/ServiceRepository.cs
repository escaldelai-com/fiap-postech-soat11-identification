using AutoMapper;
using MongoDB.Driver;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Cache;
using Restaurant.Identification.Application.Interfaces.Repository;
using Restaurant.Identification.Data.Model;

namespace Restaurant.Identification.Data.Repositories;

public class ServiceRepository(
    IMapper mapper,
    IServiceCache cache,
    IMongoDatabase context) : IServiceRepository
{

    private readonly IMongoCollection<ServiceData> collection =
        context.GetCollection<ServiceData>("service");

    public async Task<ServiceDto?> GetById(string serviceId)
    {
        var dto = await cache.GetById(serviceId);

        if (dto != null)
            return dto;

        var data = await collection
            .Find(c => c.Id == serviceId)
            .FirstOrDefaultAsync();

        if (data == null)
            return null;

        dto = mapper.Map<ServiceDto>(data);
        await cache.SetService(dto);

        return dto;
    }

}
