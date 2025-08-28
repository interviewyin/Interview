using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interview.Models.Common.Constants;

namespace API.Interview.Models;

public class APIResponse
{
    protected readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
    /// <summary>
    /// ID received for the request
    /// </summary>
    /// <example>e46710c4-f043-441a-8787-cb38e4067091</example>
    [JsonPropertyOrder(-2)]
    public string? SubmissionID { get; set; }
    /// <summary>
    /// The date and time the request was received
    /// </summary>
    [JsonPropertyOrder(-2)]
    public DateTime ReceivedDateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Name of the endpoint the request was sent to
    /// </summary>
    [JsonPropertyOrder(-2)]
    public string? APIEndpointName { get; set; }
    /// <summary>
    /// Indicates if the request was successful or not.
    /// </summary>
    /// <example>true</example>
    [JsonPropertyOrder(-2)]
    public bool IsSuccessful { get; set; } = false;
    /// <summary>
    /// Displays the error message if there is one.
    /// </summary>
    /// <example>Failed to process request.</example>
    [JsonPropertyOrder(-2)]
    public string? ErrorMessage { get; set; }
    /// <summary>
    /// The name of the product that is sending the request
    /// </summary>
    /// <example>ML Consumer</example>
    [JsonPropertyOrder(-2)]
    public string? RequestorName { get; set; }
    /// <summary>
    /// The client name.
    /// </summary>
    /// <example>MLCommunityBank</example>
    [JsonPropertyOrder(-2)]
    public string? ClientIdentifier { get; set; }
    /// <summary>
    /// The target system name.
    /// </summary>
    /// <example>SymXChange</example>
    [JsonPropertyOrder(-2)]
    public TargetSystemType? TargetSystemType { get; set; }
}