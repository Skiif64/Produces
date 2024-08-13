namespace Produces.Core.Errors;

/// <summary>
/// An RFC7807 error error details
/// </summary>
public readonly partial struct Error
{
    public ErrorType ErrorType { get; }
    public string? Type { get; }
    public string? Title { get; }
    public string? Detail { get; }
    public IDictionary<string, object?>? Extensions { get; }

    public Error()
    {
        //To prevent create directly
        throw new NotSupportedException("Unable to create Error directly. Use factory methods.");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorType">Type of error <see cref="ErrorType"/></param>
    /// <param name="type">Type of error. Should be a url to documentation that describes this error</param>
    /// <param name="title">Title of error</param>
    /// <param name="detail">Detail of error</param>
    /// <param name="extensions">Custom parameters</param>
    private Error(ErrorType errorType, string? type, string? title, string? detail, IDictionary<string, object?>? extensions)
    {
        ErrorType = errorType;
        Type = type;
        Title = title;
        Detail = detail;
        Extensions = extensions;
    }
}