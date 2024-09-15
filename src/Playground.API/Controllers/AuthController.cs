using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Authentication;
using System.Text.Json;

namespace Playground.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private const string _clientId = "public-client";
    private const string _clientSecret = "qF8kLE8vRSPIdP74vEiGOmPwaElAEhR4";

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Keycloak");
    }
}
