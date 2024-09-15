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


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {

        if (request == null)
            return BadRequest();

        using var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret",_clientSecret),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", request.Username),
            new KeyValuePair<string, string>("password", request.Password)
        ]);

        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("token", content);

        httpResponseMessage.EnsureSuccessStatusCode();

        var response = await httpResponseMessage.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<KeyCloakSigninResponse>(response);

        return Ok(responseJson);
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        using var content = new FormUrlEncodedContent(
       [
           new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret",_clientSecret),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", request.RefreshToken),
        ]);

        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("token", content);

        httpResponseMessage.EnsureSuccessStatusCode();

        var response = await httpResponseMessage.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<KeyCloakSigninResponse>(response);

        // Implement token refresh logic using Keycloak
        return Ok(responseJson);
    }


    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest revokeTokenRequest)
    {
        if (revokeTokenRequest == null)
            return BadRequest();

        using var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret",_clientSecret),
            new KeyValuePair<string, string>("token",revokeTokenRequest.Token),
        ]);

        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("revoke", content);

        httpResponseMessage.EnsureSuccessStatusCode();

        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        // Implement token revocation logic using Keycloak
        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutRequset logoutRequset)
    {
        if (logoutRequset == null)
            return BadRequest();

        using var content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret",_clientSecret),
            new KeyValuePair<string, string>("refresh_token",logoutRequset.refresh_token),
        ]);

        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("logout", content);

        var response = await httpResponseMessage.Content.ReadAsStringAsync();

        // Implement logout logic using Keycloak
        return NoContent();
    }
}
