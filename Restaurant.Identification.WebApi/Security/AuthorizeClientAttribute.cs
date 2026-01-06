using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Identification.WebApi.Security;

public class AuthorizeClientAttribute : AuthorizeAttribute
{

    public AuthorizeClientAttribute() : base("client") { }

}
