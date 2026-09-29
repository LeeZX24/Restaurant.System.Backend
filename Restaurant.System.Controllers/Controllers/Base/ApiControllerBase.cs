using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.System.Controllers.Controllers.Base
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}

