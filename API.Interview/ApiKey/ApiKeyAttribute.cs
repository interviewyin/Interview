using Microsoft.AspNetCore.Mvc;

namespace API.Interview.ApiKey;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyServiceFilter))
    {
    }
}
