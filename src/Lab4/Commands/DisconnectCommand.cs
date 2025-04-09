using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class DisconnectCommand : IBasicCommand, IEquatable<DisconnectCommand>
{
    private readonly ConnectedFileSystem _fileSystem;

    public DisconnectCommand(ConnectedFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.Disconnect();
    }

    public bool Equals(DisconnectCommand? other)
    {
        return true;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DisconnectCommand)obj);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
