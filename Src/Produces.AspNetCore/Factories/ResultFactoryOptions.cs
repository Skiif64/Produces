using Produces.AspNetCore.Factories.Constructors;
using Produces.Core.Abstractions;

namespace Produces.AspNetCore.Factories;

public class ResultFactoryOptions
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
        where TConstructor : IResultConstructor
        => Register<TFor>(typeof(TConstructor));
}