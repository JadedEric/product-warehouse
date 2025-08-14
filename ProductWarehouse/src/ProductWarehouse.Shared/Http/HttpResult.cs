using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Shared.Http;

public class HttpResult(int statusCode, object? value = null, string? contentType = null) : IHttpResult
{
    public int StatusCode { get; } = statusCode;

    public object? Value { get; } = value;

    public string? ContentType { get; } = contentType ?? "application/json";

    public static HttpResult Ok(object? value = null) => new(200, value);
    
    public static HttpResult Created(object? value = null) => new(201, value);
    
    public static HttpResult NoContent() => new(204);
    
    public static HttpResult BadRequest(object? value = null) => new(400, value);
    
    public static HttpResult NotFound(object? value = null) => new(404, value);
    
    public static HttpResult Conflict(object? value = null) => new(409, value);
    
    public static HttpResult InternalServerError(object? value = null) => new(500, value);
}
