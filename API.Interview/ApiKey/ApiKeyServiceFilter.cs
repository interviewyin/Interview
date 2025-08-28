using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using API.Interview.Utils;
using API.Interview.Data;

namespace API.Interview.ApiKey;

public class ApiKeyServiceFilter(IApiKeyValidation apiKeyValidation, ILoggerFactory loggerFactory) : IAsyncAuthorizationFilter
{
    private readonly ILogger _logger = loggerFactory.CreateLogger(typeof(ApiKeyServiceFilter));

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!DotNetConfiguration.IsApiKeyRequired(context.HttpContext))
        {
            _logger.LogInformation("API Key is not required");
            return Task.CompletedTask;
        }

        _logger.LogInformation("fetching '{KeyForApiKey}' request header", Constants.RequestHeaderApiKey);
        StringValues headerValues = context.HttpContext.Request.Headers[Constants.RequestHeaderApiKey];
        var apiKey = headerValues.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogInformation("API key is required");
            context.Result = new ObjectResult(new { error = "API key is required" }) { StatusCode = APIStatusCodes.Status400 };
        }
        else if (!apiKeyValidation.IsValidApiKey(apiKey))
        {
            var apiShortKey = ApiKeyUtils.GetApiShortKey(apiKey);
            _logger.LogInformation("'{ApiShortKey}' is not a valid API key", apiShortKey);
            context.Result = new ObjectResult(new { error = "API Key is not valid" }) { StatusCode = APIStatusCodes.Status401 };
        }
        else
        {
            _logger.LogInformation("API key is valid");
        }

        return Task.CompletedTask;
    }
}
