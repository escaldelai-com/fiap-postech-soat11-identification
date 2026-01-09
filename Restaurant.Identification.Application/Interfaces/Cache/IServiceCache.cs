using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Cache;

public interface IServiceCache
{

    Task<ServiceDto?> Get(string? serviceId);

    Task Set(ServiceDto service);

}
