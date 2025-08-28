using API.Interview.ApiKey;
using API.Interview.Services;
using API.Interview.Utils;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// API key services
builder.Services.AddScoped<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ApiKeyServiceFilter>();
builder.Services.AddSingleton<IInterviewRepository, InMemoryInterviewRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Interview", Version = "v1" });
    c.OperationFilter<ApiKeySwaggerOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline. Enable Swagger for all environments for interview convenience.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Exception handler to route to a consistent error response
app.UseExceptionHandler("/ErrorHandling/Error");

app.MapControllers();

// Redirect root to Swagger for convenience
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

// Make Program class visible for tests
public partial class Program { }
