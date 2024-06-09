using System.Reflection;

namespace Produces.Example.Endpoints;

public static class EndpointExtensions
{
    public static WebApplication UseEndpoint(this WebApplication app, params Assembly[] assemblies)
    {
        var endpoints = assemblies
            .SelectMany(types => types.DefinedTypes)
            .Where(type => type.IsClass && !type.IsAbstract)
            .Where(type => type.IsAssignableTo(typeof(IEndpoint)))
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>();

        foreach (var endpoint in endpoints)
        {
            endpoint.ConfigureEndpoint(app);
        }

        return app;
    }
}