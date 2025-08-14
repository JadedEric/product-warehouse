using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Shared.Http;

public class HttpResult<T>(int statusCode, T? value = default, string? contentType = null) : HttpResult(statusCode, value, contentType), IHttpResult<T>
{
    public new T? Value { get; } = value;

    public static HttpResult<T> Ok(T value) => new(200, value);
    
    public static HttpResult<T> Created(T value) => new(201, value);
    
    public static HttpResult<T> BadRequest(T? value = default) => new(400, value);
    
    public static HttpResult<T> NotFound(T? value = default) => new(404, value);
    
    public static HttpResult<T> Conflict(T? value = default) => new(409, value);
}