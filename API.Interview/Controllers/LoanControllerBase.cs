using API.Interview.Models;
using API.Interview.Models.Common.Constants;
using API.Interview.Models.Common.Mapping;
using API.Interview.Services;

namespace API.Interview.Controllers;


public abstract class LoanControllerBase : APIControllerBase
{
    protected readonly Type _parentType;
    protected readonly ILogger _logger;
    protected readonly IConfiguration _config;

    protected LoanControllerBase(ActivityType activityType, IConfiguration config, ILoggerFactory loggerFactory, IInterviewRepository repo)
        : base(activityType, loggerFactory, repo)
    {
        _parentType = GetType();
        _config = config;
        _logger = loggerFactory.CreateLogger(_parentType);
    }

    public virtual Task<TResponse> HandleBookPost<TRequest, TResponse>(TRequest request, Func<TRequest, TResponse, Client, TargetSystem, Task> processFunc)
        where TRequest : BookRequest
        where TResponse : APIResponse, new()
    {
        return base.HandlePost<TRequest, TResponse>(request, async (req, res, client, targetSystem) =>
        {
            await processFunc(req, res, client, targetSystem);
        });
    }


}
