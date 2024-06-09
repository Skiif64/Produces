using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Produces.AspNetCore.Factories.Constructors;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories;

internal sealed class ResultFactory : IResultFactory
{
    private readonly ResultFactoryOptions _options;
    private readonly IServiceProvider _provider;

    public ResultFactory(IOptions<ResultFactoryOptions> options, IServiceProvider provider)
    {
        _options = options.Value;
        _provider = provider;
    }

    public IResult Create(IProduce result, HttpContext context)
    {
        var type = result.GetType();
        if (type.IsGenericType)
        {
            var openType = type.GetGenericTypeDefinition();
            return CreateGeneric(openType, type.GetGenericArguments()[0], result, context);
        }
        return CreateNonGeneric(type, result, context);
    }

    private IResult CreateNonGeneric(Type type, IProduce result, HttpContext context)
    {
        if (!_options.Constructors.TryGetValue(type, out var constructorType))
            throw new InvalidOperationException($"Constructor for {type} is not found");
        var constructor = (IResultConstructor)_provider.GetRequiredService(constructorType);
        return constructor.Construct(result, context);
    }
    
    private IResult CreateGeneric(Type openType, Type genericType, IProduce result, HttpContext context)
    {
        if (!_options.Constructors.TryGetValue(openType, out var constructorType))
            throw new InvalidOperationException($"Constructor for {openType.MakeGenericType(genericType)} is not found");
        var constructor = (IResultConstructor)_provider.GetRequiredService(constructorType.MakeGenericType(genericType));
        return constructor.Construct(result, context);
    }
}