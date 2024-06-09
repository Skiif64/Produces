using Microsoft.Extensions.DependencyInjection;
using Produces.AspNetCore.Factories;
using Produces.AspNetCore.Factories.Constructors;
using Produces.Core;

namespace Produces.AspNetCore.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddProduces(this IServiceCollection services, Action<ResultFactoryConfigurator>? configure = null)
    {
        var configuration = new ResultFactoryConfigurator();
        configure?.Invoke(configuration);
        configuration.Register<Produce, NonGenericResultConstructor>();
        configuration.Register(typeof(Produce<>), typeof(GenericResultConstructor<>));
        configuration.RegisterConstructors(services);
        services.AddSingleton(configuration);
        services.AddSingleton<IResultFactory, ResultFactory>();
        
        return services;
    }
}