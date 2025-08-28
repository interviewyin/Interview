using System.ComponentModel.DataAnnotations;

namespace API.Interview.Models.Common.Mapping;

public class Client
{
    [Required]
    public Guid ID { get; set; } = Guid.NewGuid();
    [Required]
    public string EIPIdentifier { get; set; } = null!;
    [Required]
    public string DisplayText { get; set; } = null!;
    [Required]
    public bool IsActive { get; set; } = true;
}