using Itmo.ObjectOrientedProgramming.Lab4.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class FileShowCommand : IBasicCommand, IEquatable<FileShowCommand>
{
    private readonly IWriter _writer;

    public string Path { get; private set; }

    private FileShowCommand(IWriter writer, string path)
    {
        _writer = writer;
        Path = path;
    }

    public static Builder Build => new();

    public void Execute()
    {
        using StreamReader streamReader = new(Path);
        while (streamReader.ReadLine() is { } line)
        {
            _writer.WriteLine(line);
        }
    }

    public void ExtendPath()
    {
        Path = System.IO.Path.GetFullPath(Path);
    }

    public class Builder
    {
        private string? _path;
        private IWriter? _writer;

        public Builder WithPath(string path)
        {
            _path = path;

            return this;
        }

        public Builder WithWriter(IWriter writer)
        {
            _writer = writer;

            return this;
        }

        public CommandBuildResult Build()
        {
            return new CommandBuildResult.Success(new FileShowCommand(
                _writer ?? throw new ArgumentNullException(nameof(_writer)),
                _path ?? throw new ArgumentNullException(nameof(_path))));
        }
    }

    public bool Equals(FileShowCommand? other)
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
        return Equals((FileShowCommand)obj);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Path, StringComparison.Ordinal);
    }
}
