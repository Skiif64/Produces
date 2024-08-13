using System.Net;
using Microsoft.AspNetCore.Http;
using Produces.Core.Errors;

namespace Produces.AspNetCore.Utils;

internal static class ErrorToResultConverter
{
    private const string DefaultType = "about:blank";
    public static IResult ToErrorResult(Error error, HttpContext context)
    {
        var statusCode = GetStatusCode(error.ErrorType);
        return Results.Problem(
            type: error.Type ?? DefaultType,
            title: error.Title ?? statusCode.ToString(),
            detail: error.Detail,
            instance: context.Request.Path,
            statusCode: (int)statusCode,
            extensions: error.Extensions);
    }
    
    private static HttpStatusCode GetStatusCode(ErrorType type) => type switch
    {
        ErrorType.Problem => HttpStatusCode.BadRequest,
        ErrorType.NotFound => HttpStatusCode.NotFound,
        ErrorType.AccessDenied => HttpStatusCode.Forbidden,
        ErrorType.Conflict => HttpStatusCode.Conflict,
        ErrorType.Unexpected => HttpStatusCode.InternalServerError,
        _ => HttpStatusCode.InternalServerError
    };
}