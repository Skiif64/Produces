using Produces.Core.Errors;

namespace Produces.Core.Abstractions;

public interface IProduce
{
    public bool Failed { get; }
    public Error Error { get; }
}