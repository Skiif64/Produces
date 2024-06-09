# Produces

A simple implementation of "Result pattern". Errors are similar to [RFC7807](https://datatracker.ietf.org/doc/html/rfc7807) errors.

## Getting started

### Usage of result

Install package: Produces.Core

``` csharp

Produce<double> Divide(double a, double b)
{
    if (a == 0 || b == 0)
        return Produce.Failure<double>(Error.Problem("about:blank", "Unable to divide to 0"));
    
    return a / b;
}
```

### Use with Asp Net Core

Install package: Produces.AspNetCore

First, add produces to DI:
``` csharp

builder.Services.AddProduces();
```
And then use *ToHttpResult* method into endpoint:

``` csharp

Produce.Success().ToHttpResult(context);

```
