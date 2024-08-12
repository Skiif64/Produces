using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Produces.AspNetCore.Factories.Constructors;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories;

internal sealed class ResultFactory : IResultFactory
{
    private readonly ResultFactoryConfigurator _configurator;
    private readonly IServiceProvider _provider;

    public ResultFactory(ResultFactoryConfigurator configurator, IServiceProvider provider)
    {
        _configurator = configurator;
        _provider = provider;
    }

    public IResult Create<TProduce>(TProduce result, HttpContext context)
        where TProduce : IProduce
    {
        var type = result.GetType();
        if (type.IsGenericType)
        {
            var openType = type.GetGenericTypeDefinition();
            return CreateGeneric(openType, type.GetGenericArguments()[0], result, context);
        }
        return CreateNonGeneric(type, result, context);
    }

    private IResult CreateNonGeneric<TProduce>(Type type, TProduce result, HttpContext context)
        where TProduce : IProduce
    {
        if (!_configurator.Constructors.TryGetValue(type, out var constructorType))
            throw new InvalidOperationException($"Constructor for {type} is not found");
        var constructor = (IResultConstructor<TProduce>)_provider.GetRequiredService(constructorType);
        return constructor.Construct(result, context);
    }
    
    private IResult CreateGeneric<TProduce>(Type openType, Type genericType, TProduce result, HttpContext context)
        where TProduce : IProduce
    {
        if (!_configurator.Constructors.TryGetValue(openType, out var constructorType))
            throw new InvalidOperationException($"Constructor for {openType.MakeGenericType(genericType)} is not found");
        var constructor = (IResultConstructor<TProduce>)_provider.GetRequiredService(constructorType.MakeGenericType(genericType));
        return constructor.Construct(result, context);
    }
}