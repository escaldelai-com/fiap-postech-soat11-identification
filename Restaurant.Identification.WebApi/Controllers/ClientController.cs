using Microsoft.AspNetCore.Mvc;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Facade;

namespace Restaurant.Identification.WebApi.Controllers;

[Route("[controller]")]
public class ClientController(
    IClientFacade facade) : Controller
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    public async Task<IActionResult> GetNoIdentify()
    {
        var result = await facade.GetClientNoIdentify();

        return result != null
            ? Ok(result)
            : NotFound();
    }

    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDto>))]
    public async Task<IActionResult> GetList()
    {
        var result = await facade.GetClientList();

        return result != null
            ? Ok(result)
            : NotFound();
    }

    [HttpGet("{cpf}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ClientDto))]
    public async Task<IActionResult> GetIdentify(string cpf)
    {
        var result = await facade.GetClientIdentify(cpf);

        return result != null 
            ? Ok(result)
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(string))]
    public async Task<IActionResult> PostRegister([FromBody] ClientDto client)
    {
        var result = await facade.Register(client);

        return Ok(result);
    }

}
