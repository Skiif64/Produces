using System.Net;
using Microsoft.AspNetCore.Http;
using Produces.Core;
using Produces.Core.Abstractions;
using Produces.Core.Errors;

namespace Produces.AspNetCore.Factories.Constructors;

internal sealed class GenericResultConstructor<TValue> : BaseResultConstructor<TValue>
{
    public override IResult Construct(IProduce<TValue> result, HttpContext context)
    {
        if (!result.Failed)
            return SuccessResult(result.Value!);
        return FailureResult(result.Error!, context);
    }
    
    private static IResult SuccessResult(TValue value) => Results.Ok(value);
    
    //TODO: move outside, remove code duplicate
    private static IResult FailureResult(Error error, HttpContext context)
    {
        var statusCode = GetStatusCode(error.ErrorType);
        return Results.Problem(
            type: error.Type,
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
        _ => HttpStatusCode.InternalServerError
    };
}