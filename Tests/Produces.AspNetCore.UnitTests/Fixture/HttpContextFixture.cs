using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Produces.AspNetCore.UnitTests.Fixture;

public class HttpContextFixture
{
    private HttpContext? _context;
    public HttpContext Context => _context ??= CreateContext();
    private HttpContext CreateContext()
    {
        var context = new DefaultHttpContext();
        var services = CreateServiceProvider();
        context.RequestServices = services;
        return context;
    }

    private IServiceProvider CreateServiceProvider()
    {
        var collection = new ServiceCollection();

        collection.AddLogging(cfg => cfg.AddProvider(NullLoggerProvider.Instance));
        
        return collection.BuildServiceProvider();
    }
}