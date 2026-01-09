using Microsoft.AspNetCore.Mvc;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Facade;

namespace Restaurant.Identification.WebApi.Controllers;

[Route("[controller]")]
public class ServiceController(
    IServiceFacade facade) : Controller
{

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(ServiceLoginDto login)
    {
        var result = await facade.Login(login);

        return Ok(result);
    }

}
