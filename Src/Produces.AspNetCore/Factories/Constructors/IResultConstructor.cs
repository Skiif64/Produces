using Microsoft.AspNetCore.Http;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories.Constructors;

public interface IResultConstructor<in TProduce>
    where TProduce : IProduce
{
    IResult Construct(TProduce result, HttpContext context);
}