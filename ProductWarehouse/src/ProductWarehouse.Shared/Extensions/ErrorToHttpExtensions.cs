using ProductWarehouse.Shared.Common;
using ProductWarehouse.Shared.Http;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Shared.Extensions;

public static class ErrorToHttpExtensions
{
    private static readonly Dictionary<string, int> ErrorToStatusCodeMappings = new()
    {
        ["Validation"] = 400,
        ["NotFound"] = 404,
        ["Conflict"] = 409,
        ["Unauthorized"] = 401,
        ["Forbidden"] = 403,
        ["General"] = 500
    };

    public static IHttpResult ToHttpResult(this Error error)
    {
        var statusCode = GetStatusCodeFromError(error);
        var problemDetails = error.ToProblemDetails();

        return new HttpResult(statusCode, problemDetails);
    }

    public static IHttpResult<T> ToHttpResult<T>(this Error error)
    {
        var statusCode = GetStatusCodeFromError(error);
        return new HttpResult<T>(statusCode, default);
    }

    private static int GetStatusCodeFromError(Error error)
    {
        var errorCategory = error.Code.Split('.').FirstOrDefault() ?? "General";

        return ErrorToStatusCodeMappings.TryGetValue(errorCategory, out var statusCode)
            ? statusCode
            : 500;
    }

    private static object ToProblemDetails(this Error error)
    {
        _ = error.Code.Split('.').FirstOrDefault() ?? "General";
        var statusCode = GetStatusCodeFromError(error);

        return new
        {
            type = $"https://tools.ietf.org/html/rfc9110#section-{GetRfcSection(statusCode)}",
            title = GetTitleFromStatusCode(statusCode),
            status = statusCode,
            detail = error.Description,
            code = error.Code
        };
    }

    private static string GetRfcSection(int statusCode) => statusCode switch
    {
        400 => "15.5.1",
        401 => "15.5.2",
        403 => "15.5.4",
        404 => "15.5.5",
        409 => "15.5.10",
        _ => "15.6.1"
    };
    
    private static string GetTitleFromStatusCode(int statusCode) => statusCode switch
    {
        400 => "Bad Request",
        401 => "Unauthorized",
        403 => "Forbidden",
        404 => "Not Found",
        409 => "Conflict",
        _ => "Internal Server Error"
    };
}
