using Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(LogUserActivity))]
public class BaseApiController : ControllerBase
{
    


}