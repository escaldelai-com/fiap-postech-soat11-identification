using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Cache;

public interface IServiceCache
{

    Task<ServiceDto?> GetById(string? serviceId);

    Task SetService(ServiceDto service);

}
