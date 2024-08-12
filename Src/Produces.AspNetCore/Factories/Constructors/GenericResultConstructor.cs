using Microsoft.AspNetCore.Http;
using Produces.AspNetCore.Utils;
using Produces.Core;

namespace Produces.AspNetCore.Factories.Constructors;

internal sealed class GenericResultConstructor<TValue> : IResultConstructor<Produce<TValue>>
{
    public IResult Construct(Produce<TValue> result, HttpContext context)
    {
        if (!result.Failed)
            return SuccessResult(result.Value!);
        return ErrorToResultConverter.ToErrorResult(result.Error, context);
    }
    private static IResult SuccessResult(TValue value) => Results.Ok(value);
    
}