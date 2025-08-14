using ProductWarehouse.Shared.Common;
using ProductWarehouse.Shared.Http;
using ProductWarehouse.Shared.Interfaces;

namespace ProductWarehouse.Shared.Extensions;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }

    public static TOut Match<T, TOut>(
        this Result<T> result,
        Func<T, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
    }

    public static Result<TOut> Map<T, TOut>(
        this Result<T> result,
        Func<T, TOut> mapper)
    {
        return result.IsSuccess
            ? Result.Success(mapper(result.Value))
            : Result.Failure<TOut>(result.Error);
    }

    public static Result<TOut> Bind<T, TOut>(
        this Result<T> result,
        Func<T, Result<TOut>> binder)
    {
        return result.IsSuccess
            ? binder(result.Value)
            : Result.Failure<TOut>(result.Error);
    }

    public static Result<T> Tap<T>(
        this Result<T> result,
        Action<T> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }
        return result;
    }

    public static IHttpResult ToHttpResult(this Result result)
    {
        return result.Match(
            onSuccess: () => HttpResult.NoContent(),
            onFailure: error => error.ToHttpResult());
    }

    public static IHttpResult<T> ToHttpResult<T>(this Result<T> result)
    {
        return result.Match(
            onSuccess: value => HttpResult<T>.Ok(value),
            onFailure: error => error.ToHttpResult<T>());
    }

    public static IHttpResult ToHttpResult(this Result result, Func<IHttpResult> onSuccess)
    {
        return result.Match(
            onSuccess: onSuccess,
            onFailure: error => error.ToHttpResult());
    }

    public static IHttpResult ToHttpResult<T>(this Result<T> result, Func<T, IHttpResult> onSuccess)
    {
        return result.Match(
            onSuccess: onSuccess,
            onFailure: error => error.ToHttpResult());
    }
}