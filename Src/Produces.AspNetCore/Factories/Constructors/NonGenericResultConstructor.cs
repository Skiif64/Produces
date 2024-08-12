using Microsoft.AspNetCore.Http;
using Produces.AspNetCore.Utils;
using Produces.Core;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories.Constructors;

internal sealed class NonGenericResultConstructor : IResultConstructor<Produce>
{
    public IResult Construct(Produce result, HttpContext context)
    {
        if (!result.Failed)
            return SuccessResult();
        return ErrorToResultConverter.ToErrorResult(result.Error, context);
    }
    
    private static IResult SuccessResult() => Results.NoContent();
    
}