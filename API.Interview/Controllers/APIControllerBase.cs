using API.Interview.Data;
using API.Interview.Models;
using API.Interview.Models.Common.Constants;
using API.Interview.Models.Common.Mapping;
using API.Interview.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Interview.Controllers;

[SwaggerResponse(APIStatusCodes.Status200, "Everything worked as expected.", typeof(APIResponse))]
[SwaggerResponse(APIStatusCodes.Status400, "The request was unacceptable, usually caused by a missing a required parameter.", typeof(APIResponse))]
[SwaggerResponse(APIStatusCodes.Status401, "No valid API key provided.", typeof(APIResponse))]
[SwaggerResponse(APIStatusCodes.Status403, "The API key doesn't have permissions to perform the request.", typeof(APIResponse))]
[SwaggerResponse(APIStatusCodes.Status404, "The requested resource doesn't exist.")]
[SwaggerResponse(APIStatusCodes.Status405, "The Method is not allowed. Usually caused by the Target System not having the Activity available.")]
[SwaggerResponse(APIStatusCodes.Status409, "The request conflicts with another request (perhaps because two requests are using the same key).")]
[SwaggerResponse(APIStatusCodes.Status429, "Too many requests.")]
[SwaggerResponse(APIStatusCodes.Status500, "Something went wrong at the EIP server. The most common cause is misconfiguration at the server for the specified client financial institution.", typeof(APIResponse))]
[SwaggerResponse(APIStatusCodes.Status502, "Something went wrong at the target system.", typeof(APIResponse))]
[ApiController]
public abstract class APIControllerBase : ControllerBase
{
    protected readonly ILoggerFactory _loggerFactory;
    protected readonly ActivityType _activityType;
    protected readonly IInterviewRepository _repo;

    protected APIControllerBase(ActivityType activityType, ILoggerFactory loggerFactory, IInterviewRepository repo)
    {
        _activityType = activityType;
        _loggerFactory = loggerFactory;
        _repo = repo;
    }

    public virtual async Task<TResponse> HandlePost<TRequest, TResponse>(
        TRequest request,
        Func<TRequest, TResponse, Client, TargetSystem, Task> processFunc)
        where TRequest : APIRequest
        where TResponse : APIResponse, new()
    {
        var logger = _loggerFactory.CreateLogger(typeof(APIControllerBase));

        var response = new TResponse
        {
            SubmissionID = request.SubmissionID,
            APIEndpointName = Request.Path,
            IsSuccessful = true,
            RequestorName = request.RequestorName,
            ClientIdentifier = request.ClientIdentifier,
            TargetSystemType = request.TargetSystemType
        };

        try
        {
            var client = await _repo.GetClientByIdentifier(request.ClientIdentifier);
            if (client is null || !client.IsActive)
            {
                return ProcessErrorResponse(response, "ClientID not found", APIStatusCodes.Status404);
            }

            var targetSystem = await _repo.GetTargetSystem(request.TargetSystemType);
            if (targetSystem is null)
            {
                return ProcessErrorResponse(response, "TargetSystemID not found", APIStatusCodes.Status404);
            }

            if (!await _repo.IsClientEnabledForTargetSystem(client.ID, targetSystem.ID))
            {
                return ProcessErrorResponse(response, $"Client '{client.DisplayText}' is not configured to use Target System '{targetSystem.DisplayText}'", APIStatusCodes.Status500);
            }

            if (!await _repo.IsActivityEnabledForClientTargetSystem(client.ID, targetSystem.ID, _activityType))
            {
                return ProcessErrorResponse(response, $"Client '{client.DisplayText}' is not configured to use Target System '{targetSystem.DisplayText}' and Activity '{_activityType}'", APIStatusCodes.Status500);
            }

            await processFunc.Invoke(request, response, client, targetSystem);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;
            logger.LogError(ex, "Error processing EIP request: {ErrorMessage}", errorMessage);
            return ProcessErrorResponse(response, errorMessage, APIStatusCodes.Status500);
        }

        return ProcessResponse(response);
    }

    private TResponse ProcessErrorResponse<TResponse>(TResponse response, string errorMessage, int eipStatusCode)
        where TResponse : APIResponse
    {
        response.IsSuccessful = false;
        response.ErrorMessage = errorMessage;
        Response.StatusCode = eipStatusCode;
        return ProcessResponse(response);
    }

    private TResponse ProcessResponse<TResponse>(TResponse response)
        where TResponse : APIResponse
    {
        var logger = _loggerFactory.CreateLogger(typeof(APIControllerBase));
        logger.LogDebug("EIP Response Sent: {@APIResponse}", response);
        return response;
    }
}
