namespace API.Interview.ApiKey;

public interface IApiKeyValidation
{
    bool IsValidApiKey(string submittedApiKey);
}
