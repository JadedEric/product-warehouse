namespace ProductWarehouse.Shared.Interfaces;

public interface IHttpResult
{
    int StatusCode { get; }

    object? Value { get; }

    string? ContentType { get; }
}
