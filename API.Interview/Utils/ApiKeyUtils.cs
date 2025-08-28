using System.Security.Cryptography;
using System.Text;

namespace API.Interview.Utils;

public static class ApiKeyUtils
{
    // Derive a short fingerprint for logs (never log full key)
    public static string GetApiShortKey(string apiKey)
        => apiKey.Length <= 8 ? apiKey : $"{apiKey[..4]}…{apiKey[^4..]}";

    // Very simple prefix rule to mimic prod/non-prod keys, e.g., "sk-dev_", "sk-prod_"
    public static bool IsValidApiKeyPrefix(string apiKey, string environmentName)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) return false;

        var isProd = string.Equals(environmentName, "Production", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(environmentName, "Prod", StringComparison.OrdinalIgnoreCase);

        return isProd ? apiKey.StartsWith("sk-prod_", StringComparison.Ordinal) : apiKey.StartsWith("sk-dev_", StringComparison.Ordinal);
    }

    // Stable SHA256 hash for config comparison
    public static string HashApiKey(string apiKey)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
