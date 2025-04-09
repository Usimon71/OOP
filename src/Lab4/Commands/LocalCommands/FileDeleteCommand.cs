namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class FileDeleteCommand : IBasicCommand, IEquatable<FileDeleteCommand>
{
    public string Path { get; private set; }

    private FileDeleteCommand(string path)
    {
        Path = path;
    }

    public static Builder Build => new();

    public void Execute()
    {
        File.Delete(Path);
    }

    public void ExtendPath()
    {
        Path = System.IO.Path.GetFullPath(Path);
    }

    public class Builder
    {
        private string? _path;

        public Builder WithPath(string path)
        {
            _path = path;

            return this;
        }

        public CommandBuildResult Build()
        {
            return new CommandBuildResult.Success(new FileDeleteCommand(
                _path ?? throw new ArgumentNullException(nameof(_path))));
        }
    }

    public bool Equals(FileDeleteCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FileDeleteCommand)obj);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Path, StringComparison.Ordinal);
    }
}
