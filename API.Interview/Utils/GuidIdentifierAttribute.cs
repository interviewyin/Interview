using System.Diagnostics.CodeAnalysis;

namespace API.Interview.Utils;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Field)]
public class GuidIdentifierAttribute : Attribute
{
    public string GuidString { get; set; } = string.Empty;
    public Guid Guid
    {
        get
        {
            if (!Guid.TryParse(GuidString, out var result))
            {
                return Guid.Empty;
            }

            return result;
        }
    }
}
