using Produces.Core.Errors;

namespace Produces.Core;

public readonly partial struct Produce
{
    public static Produce Success() => new Produce(false, null);
    public static Produce Failure(Error error) => new Produce(true, error);
    
    public static Produce<TValue> Success<TValue>(TValue value) => new Produce<TValue>(false, null, value);
    public static Produce<TValue> Failure<TValue>(Error error) => new Produce<TValue>(true, error, default);
}