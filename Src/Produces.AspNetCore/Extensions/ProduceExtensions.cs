using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Produces.AspNetCore.Factories;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Extensions;

public static class ProduceExtensions
{
    public static IResult ToHttpResult(this IProduce produce, HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(produce, nameof(produce));
        var factory = context.RequestServices.GetRequiredService<IResultFactory>();
        return factory.Create(produce, context);
    }
}