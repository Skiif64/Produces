namespace Produces.Core.Errors;

public readonly struct ValidationFailure
{
    public string Name { get; }
    public string Reason { get; }
    public object? AttemptedValue { get; }

    public ValidationFailure(string name, string reason, object? attemptedValue)
    {
        Name = name;
        Reason = reason;
        AttemptedValue = attemptedValue;
    }
}