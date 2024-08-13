using System.Net;
using Microsoft.AspNetCore.Http;
using Produces.AspNetCore.Factories.Constructors;
using Produces.AspNetCore.UnitTests.Fixture;
using Produces.Core;
using Produces.Core.Errors;

namespace Produces.AspNetCore.UnitTests.Tests;

public sealed class NonGenericResultConstructorTests : ResultTestBase
{
    public NonGenericResultConstructorTests(HttpContextFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task Construct_ShouldReturn204_WhenProduceSuccess()
    {
        var produce = Produce.Success();
        var constructor = new NonGenericResultConstructor();
        
        var result = constructor.Construct(produce, Context);
        await result.ExecuteAsync(Context);
        
        Assert.Equal((int)HttpStatusCode.NoContent, Context.Response.StatusCode);
    }
    
    [Theory]
    [InlineData(HttpStatusCode.BadRequest, ErrorType.Problem)]
    [InlineData(HttpStatusCode.NotFound, ErrorType.NotFound)]
    [InlineData(HttpStatusCode.Conflict, ErrorType.Conflict)]
    [InlineData(HttpStatusCode.Unauthorized, ErrorType.AccessDenied)]
    [InlineData(HttpStatusCode.InternalServerError, ErrorType.Unexpected)]
    public async Task Construct_ShouldReturnStatusCode_WhenProduceFailWithType(HttpStatusCode statusCode, ErrorType type)
    {
        var produce = Produce.Failure(Error.Create(type));
        var constructor = new NonGenericResultConstructor();
        
        var result = constructor.Construct(produce, Context);
        await result.ExecuteAsync(Context);
        
        Assert.Equal((int)statusCode, Context.Response.StatusCode);
    }
}