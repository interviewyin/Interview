namespace API.Interview.Data;

public static class APIStatusCodes
{
    public const int Status200 = StatusCodes.Status200OK;
    public const int Status400 = StatusCodes.Status400BadRequest;
    public const int Status401 = StatusCodes.Status401Unauthorized;
    public const int Status403 = StatusCodes.Status403Forbidden;
    public const int Status404 = StatusCodes.Status404NotFound;
    public const int Status405 = StatusCodes.Status405MethodNotAllowed;
    public const int Status409 = StatusCodes.Status409Conflict;
    public const int Status429 = StatusCodes.Status429TooManyRequests;
    public const int Status500 = StatusCodes.Status500InternalServerError;
    public const int Status502 = StatusCodes.Status502BadGateway;
}
