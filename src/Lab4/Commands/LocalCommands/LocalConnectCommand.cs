using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class LocalConnectCommand : IBasicCommand, IEquatable<LocalConnectCommand>
{
    private readonly ConnectedFileSystem _fileSystem;

    public string Path { get; private set; }

    private LocalConnectCommand(string path, ConnectedFileSystem fileSystem)
    {
        Path = path;
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.Connect(Path);
    }

    public static Builder Build => new();

    public class Builder
    {
        private string? _path;
        private ConnectedFileSystem? _fileSystem;

        public Builder WithPath(string path)
        {
            _path = path;

            return this;
        }

        public Builder WithFileSystem(ConnectedFileSystem fileSystem)
        {
            _fileSystem = fileSystem;

            return this;
        }

        public CommandBuildResult Build()
        {
            return new CommandBuildResult.Success(new LocalConnectCommand(
                _path ?? throw new ArgumentNullException(nameof(_path)),
                _fileSystem ?? throw new ArgumentNullException(nameof(_fileSystem))));
        }
    }

    public bool Equals(LocalConnectCommand? other)
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
        return Equals((LocalConnectCommand)obj);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Path, StringComparison.Ordinal);
    }
}
