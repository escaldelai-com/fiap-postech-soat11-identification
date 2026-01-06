using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Identification.WebApi.Security;

public class AuthorizeAdminAttribute : AuthorizeAttribute
{

    public AuthorizeAdminAttribute() : base("admin") { }

}
