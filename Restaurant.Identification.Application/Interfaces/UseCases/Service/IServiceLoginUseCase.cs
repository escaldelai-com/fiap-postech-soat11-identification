using Restaurant.Identification.Application.DTO;

namespace Restaurant.Identification.Application.Interfaces.UseCases;

public interface IServiceLoginUseCase
{

    Task<ServiceDto?> Login(ServiceLoginDto loginDto);

}
