using System.Diagnostics.CodeAnalysis;
using Produces.Core.Abstractions;
using Produces.Core.Errors;

namespace Produces.Core;

public readonly struct Produce<TValue> : IProduce<TValue>
{
    private readonly Error? _error;
    private readonly TValue? _value;
    
    [MemberNotNullWhen(true, nameof(_error))]
    [MemberNotNullWhen(false, nameof(_value))]
    public bool Failed { get; }

    public Error Error => Failed ? _error.Value : throw new InvalidOperationException("Unable to get error of success result");
    
    public TValue Value => !Failed ? _value : throw new InvalidOperationException("Unable to get value of failed result");

    public Produce()
    {
        //To prevent create directly
        throw new NotSupportedException("Unable to create Produce directly. Use factory methods.");
    }
    
    internal Produce(bool failed, Error? error, TValue? value)
    {
        if (failed) ArgumentNullException.ThrowIfNull(error, nameof(error));
        if(!failed) ArgumentNullException.ThrowIfNull(value, nameof(value));
        _error = error;
        Failed = failed;
        _value = value;
    }
    
    public static implicit operator Produce<TValue>(Error error) => new Produce<TValue>(true, error, default);
    public static implicit operator Produce<TValue>(TValue value) => new Produce<TValue>(false, null, value);
}