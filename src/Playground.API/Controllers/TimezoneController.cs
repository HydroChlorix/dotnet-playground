using Microsoft.AspNetCore.Mvc;
using Playground.API.Timezone;
using Swashbuckle.AspNetCore.Annotations;

namespace Playground.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TimezoneController : ControllerBase
{
    private readonly ILogger<TimezoneController> _logger;

    public TimezoneController(ILogger<TimezoneController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timezone">"ICT","GMT","PDT"</param>
    /// <returns></returns>
    [HttpGet(Name = "GetTime")]
    public DateTimeOffset Get([FromQuery] string timezone)
    {
        var queryHandler = new TimeZoneQueryHandler();

        return queryHandler.Handle(new GetDateTimeOffsetQuery(timezone));

    }
}
