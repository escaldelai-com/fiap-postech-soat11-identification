using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.Repository;

public interface IServiceRepository
{

    Task<ServiceDto?> Get(string serviceId);

}
