namespace ProductWarehouse.Shared.Common;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error Validation(string field, string message) =>
        new($"Validation.{field}", message);

    public static Error NotFound(string resource, object identifier) =>
        new($"{resource}.NotFound", $"The {resource.ToLower()} with identifier '{identifier}' was not found");

    public static Error Conflict(string resource, string reason) =>
        new($"{resource}.Conflict", $"The {resource.ToLower()} cannot be processed: {reason}");
}
