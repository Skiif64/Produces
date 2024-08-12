using System.Reflection;

namespace Produces.Core.Errors;

public readonly partial struct Error
{
    private const string ValidationErrorTitle = "Your request parameters did not pass validation";

    public static Error Conflict(string type, string title, string? details = null, object? extensions = null)
        => new (ErrorType.Conflict, type, title, details, ToDictionary(extensions));
    
    public static Error Conflict(string type, string title, string? details = null, IDictionary<string, object?>? extensions = null)
        => new (ErrorType.Conflict, type, title, details, extensions);
    
    public static Error Conflict()
        => new (ErrorType.Conflict, null, null, null, null);
    
    public static Error Problem(string type, string title, string? details = null, object? extensions = null)
        => new (ErrorType.Problem, type, title, details, ToDictionary(extensions));
    
    public static Error Problem(string type, string title, string? details = null, IDictionary<string, object?>? extensions = null)
        => new (ErrorType.Problem, type, title, details, extensions);
    
    public static Error Problem()
        => new (ErrorType.Problem, null, null, null, null);
    
    public static Error NotFound(string type, string title, string? details = null, object? extensions = null)
        => new (ErrorType.NotFound, type, title, details, ToDictionary(extensions));
    
    public static Error NotFound(string type, string title, string? details = null, IDictionary<string, object?>? extensions = null)
        => new (ErrorType.NotFound, type, title, details, extensions);
    
    public static Error NotFound()
        => new (ErrorType.NotFound, null, null, null, null);
    
    public static Error AccessDenied(string type, string title, string? details = null, object? extensions = null)
        => new (ErrorType.AccessDenied, type, title, details, ToDictionary(extensions));
    
    public static Error AccessDenied(string type, string title, string? details = null, IDictionary<string, object?>? extensions = null)
        => new (ErrorType.AccessDenied, type, title, details, extensions);
    
    public static Error AccessDenied()
        => new (ErrorType.AccessDenied, null, null, null, null);

    public static Error Validation(string type, IEnumerable<ValidationFailure> errors)
        => new (ErrorType.Problem, type, ValidationErrorTitle, null, new Dictionary<string, object?>()
        {
            ["errors"] = errors
        });
    
    public static Error Unexpected(string type, string title, string? details = null, object? extensions = null)
        => new (ErrorType.Unexpected, type, title, details, ToDictionary(extensions));
    
    public static Error Unexpected(string type, string title, string? details = null, IDictionary<string, object?>? extensions = null)
        => new (ErrorType.Unexpected, type, title, details, extensions);

    public static Error Unexpected() 
        => new(ErrorType.Unexpected, null, null, null, null);

    public static Error Create(ErrorType errorType, string type, string title, string? details = null, object? extensions = null)
        => new(errorType, type, title, details, ToDictionary(extensions));
    
    public static Error Create(ErrorType errorType, string type, string title, string? details = null, IDictionary<string, object?>? extensions = null)
        => new(errorType, type, title, details, extensions);

    public static Error Create(ErrorType errorType)
        => new Error(errorType, null, null, null, null);
    
    
    private static IDictionary<string,object?>? ToDictionary(object? extensions)
        => extensions?.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(key => key.Name, value => value.GetValue(extensions));
}