using Microsoft.AspNetCore.Http;
using Produces.AspNetCore.Utils;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories.Constructors;

internal sealed class GenericResultConstructor<TValue> : BaseResultConstructor<TValue>
{
    public override IResult Construct(IProduce<TValue> result, HttpContext context)
    {
        if (!result.Failed)
            return SuccessResult(result.Value!);
        return ErrorToResultConverter.ToErrorResult(result.Error, context);
    }
    
    private static IResult SuccessResult(TValue value) => Results.Ok(value);
}