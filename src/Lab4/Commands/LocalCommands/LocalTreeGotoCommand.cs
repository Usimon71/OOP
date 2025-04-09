namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class LocalTreeGotoCommand : IBasicCommand, IEquatable<LocalTreeGotoCommand>
{
    public static Builder Build => new();

    public string NewPath { get; private set; }

    private LocalTreeGotoCommand(string newPath)
    {
        NewPath = newPath;
    }

    public void Execute()
    {
        Directory.SetCurrentDirectory(NewPath);
    }

    public void ExtendPath()
    {
        NewPath = Path.GetFullPath(NewPath);
    }

    public class Builder
    {
        private string? _newPath;

        public Builder WithNewPath(string newPath)
        {
            _newPath = newPath;

            return this;
        }

        public CommandBuildResult Build()
        {
            return new CommandBuildResult.Success(
                new LocalTreeGotoCommand(
                    _newPath ?? throw new ArgumentNullException(nameof(_newPath))));
        }
    }

    public bool Equals(LocalTreeGotoCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return NewPath == other.NewPath;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LocalTreeGotoCommand)obj);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(NewPath, System.StringComparison.Ordinal);
    }
}
