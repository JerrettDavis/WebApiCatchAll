using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostContext, configuration) => 
    configuration.ReadFrom.Configuration(hostContext.Configuration));

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

var app = builder.Build();
app.UseHttpLogging();
app.MapMethods("/{*slug}", HttpMethods, 
        (string? slug, [FromBody] JsonElement? body) =>
        {
            if (body!.Value.ValueKind == JsonValueKind.Undefined) 
                return $"Hello {slug ?? "World"}!";
            
            var json = JsonObject.Create(body.Value)!.ToJsonString();
            return $"Hello {slug ?? "World"}! Here's your body: \r\n{json}";
        })
    .WithName("CatchAll");

app.Run();


public partial class Program
{
    public static readonly string AppName = typeof(Program).Namespace!;
    private static readonly string[] HttpMethods = ["GET", "POST", "PUT", "DELETE", "PATCH", "HEAD", "OPTIONS"];
}