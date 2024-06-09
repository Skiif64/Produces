using Microsoft.AspNetCore.Http;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories.Constructors;

public interface IResultConstructor
{
    IResult Construct(IProduce result, HttpContext context);
}

public interface IResultConstructor<in TValue>: IResultConstructor
{
    IResult Construct(IProduce<TValue> result, HttpContext context);
}