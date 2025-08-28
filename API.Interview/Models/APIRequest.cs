using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using API.Interview.Models.Common.Constants;

namespace API.Interview.Models;

public class APIRequest
{
    protected readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };

    /// <summary>
    /// ID for the request
    /// </summary>
    /// <example>e46710c4-f043-441a-8787-cb38e4067091</example>
    [Required]
    public string SubmissionID { get; set; } = Guid.NewGuid().ToString();
    public string APIEndpointName { get; set; } = string.Empty;
    /// <summary>
    /// The name of the product that is sending the request
    /// </summary>
    /// <example>ML Consumer</example>
    [Required]
    public string RequestorName { get; set; } = string.Empty;

    /// <summary>
    /// The ID assigned to the client
    /// </summary>
    /// <example>MLCommunityBank</example>
    public string ClientIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// The target system used to process the request
    /// </summary>
    /// <example>SymXChange</example>
    public TargetSystemType TargetSystemType { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, _serializerOptions);
    }
}
