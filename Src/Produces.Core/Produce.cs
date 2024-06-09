using System.Diagnostics.CodeAnalysis;
using Produces.Core.Abstractions;
using Produces.Core.Errors;

namespace Produces.Core;

public readonly partial struct Produce : IProduce
{
    private readonly Error? _error;
    [MemberNotNullWhen(true, nameof(_error))]
    public bool Failed { get; }
    public Error Error => Failed ? _error.Value : throw new InvalidOperationException("Unable to get error of success result");

    public Produce()
    {
        //To prevent create directly
        throw new NotSupportedException("Unable to create Produce directly. Use factory methods.");
    }
    
    private Produce(bool failed, Error? error)
    {
        if (failed) ArgumentNullException.ThrowIfNull(error, nameof(error));
        _error = error;
        Failed = failed;
    }
    
    public static implicit operator Produce(Error error) => new Produce(true, error);
}