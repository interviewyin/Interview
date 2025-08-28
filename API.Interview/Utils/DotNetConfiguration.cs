namespace API.Interview.Utils;

public static class DotNetConfiguration
{
    // Returns whether API key is required. Default true, can be disabled via config/env for local runs.
    public static bool IsApiKeyRequired(HttpContext httpContext)
    {
        var cfg = httpContext.RequestServices.GetRequiredService<IConfiguration>();
        var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

        // appsettings: { "Security": { "RequireApiKey": true } }
        var fromConfig = cfg.GetValue<bool?>("Security:RequireApiKey");
        if (fromConfig.HasValue)
        {
            return fromConfig.Value;
        }

        // Default: required unless Development
        return !env.IsDevelopment();
    }
}
