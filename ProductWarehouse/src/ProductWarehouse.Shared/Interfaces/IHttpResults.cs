namespace ProductWarehouse.Shared.Interfaces;

public interface IHttpResult<out T> : IHttpResult
{
    new T? Value { get; }
}
