using System.Security.Cryptography;
using System.Text;
using API.Interview.Utils;

namespace API.Interview.ApiKey;

public class ApiKeyValidation(IConfiguration configuration, IWebHostEnvironment environment) : IApiKeyValidation
{
    public bool IsValidApiKey(string submittedApiKey)
    {
        // 1) Prefix must align with environment (simulates short-vs-long and env variants)
        if (!ApiKeyUtils.IsValidApiKeyPrefix(submittedApiKey, environment.EnvironmentName))
        {
            return false;
        }

        // 2) If a whitelist of hashes is configured, require a match; else accept the prefix-only (for local/demo)
        var allowedHashes = configuration.GetSection("ApiKeys:AllowedHashes").Get<string[]>() ?? Array.Empty<string>();
        if (allowedHashes.Length == 0)
        {
            return true; // prefix-only validation in dev/playground
        }

        var hashed = ApiKeyUtils.HashApiKey(submittedApiKey);
        return allowedHashes.Contains(hashed, StringComparer.OrdinalIgnoreCase);
    }
}
