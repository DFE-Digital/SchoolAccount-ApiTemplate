using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Endpoints;

public class ProblemDetailsErrorReader
{
    private readonly Dictionary<string, string[]>? _errorDict;

    public ProblemDetailsErrorReader(ProblemDetails problemDetails)
    {
        if (problemDetails.Extensions.TryGetValue("errors", out object? errors) && errors is JsonElement jsonElement)
        {
            try
            {
                _errorDict = JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                    jsonElement.GetRawText());
            }
            catch
            {
                // ignore error
            }
        }
    }

    public bool HasErrorMessage(string key, string errorMessage)
    {
        return _errorDict?.TryGetValue(key, out string[]? messages) != null && messages.Contains(errorMessage);
    }
}
