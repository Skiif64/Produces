namespace Produces.Core.Abstractions;

public interface IProduce<out TValue> : IProduce
{
    public TValue Value { get; }
}