using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using API.Interview.Models.Common.Constants;

namespace API.Interview.Models.Common.Mapping;
/// <remarks>Excluded from code coverage because this class is data only</remarks>
[ExcludeFromCodeCoverage]
public class TargetSystem
{
    [Required]
    public Guid ID { get; set; } = Guid.NewGuid();
    [Required]
    public string EIPIdentifier { get; set; } = null!;
    [Required]
    public string DisplayText { get; set; } = null!;
}