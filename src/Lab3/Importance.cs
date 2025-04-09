namespace Itmo.ObjectOrientedProgramming.Lab3;

public record Importance
{
    private readonly int _importance;

    public Importance(string importance)
    {
        int numImportance = importance switch
        {
            "Low" => 0,
            "Medium" => 1,
            "High" => 2,
            _ => throw new ArgumentException($"Unknown importance: {importance}"),
        };

        _importance = numImportance;
    }

    public string StringRepresentation()
    {
        return _importance switch
        {
            0 => "Low",
            1 => "Medium",
            2 => "High",
            _ => throw new ArgumentOutOfRangeException(nameof(_importance), _importance, null),
        };
    }

    public static bool operator <(Importance importance1, Importance importance2)
    {
        return importance1._importance < importance2._importance;
    }

    public static bool operator >(Importance importance1, Importance importance2)
    {
        return importance2 < importance1;
    }
}
