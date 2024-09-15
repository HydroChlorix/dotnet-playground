using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Playground.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("me")]
    public Dictionary<string,string> GetMe([FromServices] ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
    }
}