using Microsoft.AspNetCore.Mvc;

namespace Playground.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomController : ControllerBase
{

    private readonly ILogger<RandomController> _logger;

    public RandomController(ILogger<RandomController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetRandomNumber")]
    public int Get()
    {
        return Random.Shared.Next(1, 100);
    }
}