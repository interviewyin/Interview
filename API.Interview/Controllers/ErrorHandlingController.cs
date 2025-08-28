using API.Interview.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Interview.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorHandlingController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Error")]
    public IActionResult HandleError()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var errorMessage = feature?.Error.Message ?? "An error occurred.";

        var response = new APIResponse
        {
            APIEndpointName = HttpContext.Request.Path,
            IsSuccessful = false,
            ErrorMessage = errorMessage
        };

        return new ObjectResult(response) { StatusCode = Response.StatusCode };
    }
}
