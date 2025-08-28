using API.Interview.ApiKey;
using API.Interview.Data;
using API.Interview.Models;
using API.Interview.Models.Common.Constants;
using API.Interview.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Interview.Controllers;

[ApiController]
[Route("api")]
[ApiKey]
public class LoanStatusController(IConfiguration config, ILoggerFactory loggerFactory, IInterviewRepository repo)
    : LoanControllerBase(ActivityType.LoanStatus, config, loggerFactory, repo)
{

    /// <summary>
    /// Returns the status of a book loan by its ID.
    /// </summary>
    /// <param name="id">Loan ID (GUID).</param>
    [HttpGet("LoanStatus")]
    [Produces("application/json")]
    [SwaggerResponse(APIStatusCodes.Status200, "Everything worked as expected.", typeof(LoanStatusDto))]
    [SwaggerResponse(APIStatusCodes.Status400)]
    [SwaggerResponse(APIStatusCodes.Status401)]
    [SwaggerResponse(APIStatusCodes.Status404)]
    public ActionResult<LoanStatusDto> GetStatus([FromQuery] string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            _logger.LogWarning("LoanStatus status requested with missing id");
            return BadRequest("Query parameter 'id' is required.");
        }

        if (!Guid.TryParse(id, out var loanId))
        {
            _logger.LogInformation("Invalid book loan id format: {Id}", id);
            return BadRequest("Query parameter 'id' must be a GUID.");
        }

        if (!LoanExists(loanId))
        {
            _logger.LogInformation("Loan not found for id {Id}", loanId);
            return NotFound();
        }

        var dto = new LoanStatusDto
        {
            Id = loanId,
            Status = "Pending",
            LastUpdatedUtc = DateTimeOffset.UtcNow
        };

        _logger.LogInformation("Returning status for loan {Id}: {Status}", dto.Id, dto.Status);
        return Ok(dto);
    }


    private static bool LoanExists(Guid id)
    {
        var last = id.ToString("N").Last();
        return last is '0' or '2' or '4' or '6' or '8' or 'a' or 'c' or 'e';
    }
}

public sealed class LoanStatusDto
{
    public Guid Id { get; init; }
    public string Status { get; init; } = "Pending";
    public DateTimeOffset LastUpdatedUtc { get; init; }
}
