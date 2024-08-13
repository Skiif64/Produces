using Microsoft.AspNetCore.Http;
using Produces.AspNetCore.UnitTests.Fixture;

namespace Produces.AspNetCore.UnitTests.Tests;

public abstract class ResultTestBase : IClassFixture<HttpContextFixture>
{
    protected HttpContext Context { get; }

    public ResultTestBase(HttpContextFixture fixture)
    {
        Context = fixture.Context;
    }
}