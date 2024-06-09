using Microsoft.Extensions.DependencyInjection;
using Produces.AspNetCore.Factories;
using Produces.AspNetCore.Factories.Constructors;
using Produces.Core;

namespace Produces.AspNetCore.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddProduces(this IServiceCollection services)
    {
        services.Configure<ResultFactoryOptions>(cfg =>
        {
            cfg.Register<Core.Produce, NonGenericResultConstructor>();
            cfg.Register(typeof(Produce<>), typeof(GenericResultConstructor<>));
        });
        services.AddSingleton<NonGenericResultConstructor>();
        services.AddSingleton(typeof(GenericResultConstructor<>));
        services.AddSingleton<IResultFactory, ResultFactory>();
        
        return services;
    }
}