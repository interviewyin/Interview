using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using API.Interview.Models.Common.Constants;

namespace API.Interview.Models;

public class BookRequest : APIRequest
{
    [Required]
    public string? AppId { get; set; } = string.Empty;

    [Required]
    public string? FirstName { get; set; } = string.Empty;

    [Required]
    public string? LastName { get; set; } = string.Empty;

    [Required]
    public DateTime? LoanDate { get; set; }

    [Required]
    public DateTime? DueDate { get; set; }
}