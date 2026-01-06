using Microsoft.AspNetCore.Mvc;
using Restaurant.Identification.Application.DTO;
using Restaurant.Identification.Application.Interfaces.Facade;
using Restaurant.Identification.WebApi.Security;

namespace Restaurant.Identification.WebApi.Controllers;

[Route("[controller]")]
public class ClientController(
    IClientFacade facade) : Controller
{

    [HttpGet]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
    public async Task<IActionResult> GetNoIdentify()
    {
        var result = await facade.GetClientNoIdentify();

        return result != null
            ? Ok(result)
            : NotFound();
    }

    [HttpGet("list")]
    [AuthorizeAdmin]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDto>))]
    public async Task<IActionResult> GetList([FromQuery]IEnumerable<string> id)
    {
        var result = await facade.GetClientList(id);

        return result != null
            ? Ok(result)
            : NotFound();
    }

    [HttpGet("cpf/{cpf}")]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ClientDto))]
    public async Task<IActionResult> GetIdentify(string cpf)
    {
        var result = await facade.GetClientIdentify(cpf);

        return result != null 
            ? Ok(result)
            : NotFound();
    }

    [HttpGet("id/{id}")]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ClientDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ClientDto))]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await facade.GetById(id);

        return result != null 
            ? Ok(result)
            : NotFound();
    }

    [HttpPost]
    [AuthorizeClient]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(string))]
    public async Task<IActionResult> PostRegister([FromBody] ClientDto client)
    {
        var result = await facade.Register(client);

        return Ok(result);
    }

}
