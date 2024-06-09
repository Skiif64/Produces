using Microsoft.AspNetCore.Http;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories.Constructors;

public abstract class BaseResultConstructor<TValue> : IResultConstructor<TValue>
{
    public abstract IResult Construct(IProduce<TValue> result, HttpContext context);

    public IResult Construct(IProduce result, HttpContext context)
    {
        if (result is not IProduce<TValue> casted)
            throw new InvalidCastException($"{result.GetType()} is not assignable to {typeof(IProduce<TValue>)}");
        return Construct(casted, context);
    }
}