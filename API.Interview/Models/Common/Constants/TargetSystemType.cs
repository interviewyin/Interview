using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Interview.Utils;

namespace API.Interview.Models.Common.Constants;

[JsonConverter(typeof(JsonStringEnumConverter))]

public enum TargetSystemType
{
    [GuidIdentifier(GuidString = "92197389-2915-444a-8339-530581cd1d33")]
    [Display(Name = "Meridianlink")]
    Meridianlink = 1,
}
