using Microsoft.Extensions.DependencyInjection;
using Produces.AspNetCore.Factories.Constructors;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories;

public class ResultFactoryConfigurator
{
    private readonly Dictionary<Type, Type> _constructors = new();

    public IReadOnlyDictionary<Type, Type> Constructors => _constructors;

    public void Register(Type resultType, Type constructorType)
        => _constructors.Add(resultType, constructorType);

    public void Register<TFor>(Type constructorType)
        where TFor : IProduce
        => Register(typeof(TFor), constructorType);

    public void Register<TFor, TConstructor>()
        where TFor : IProduce
        where TConstructor : IResultConstructor<TFor>
        => Register<TFor>(typeof(TConstructor));

    internal void RegisterConstructors(IServiceCollection services)
    {
        foreach (var constructor in _constructors.Values)
        {
            services.AddSingleton(constructor);
        }
    }
}