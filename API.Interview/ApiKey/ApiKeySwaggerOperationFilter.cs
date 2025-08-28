using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using API.Interview.Utils;

namespace API.Interview.ApiKey;

public class ApiKeySwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = Constants.RequestHeaderApiKey,
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema { Type = "string", Description = "API key" },
            Required = false
        });
    }
}
