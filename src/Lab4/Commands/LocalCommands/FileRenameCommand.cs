namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class FileRenameCommand : IBasicCommand, IEquatable<FileRenameCommand>
{
    private readonly string _newFileName;

    public string Path { get; private set; }

    private FileRenameCommand(string path, string newFileName)
    {
        Path = path;
        _newFileName = newFileName;
    }

    public static Builder Build => new();

    public void Execute()
    {
        string? targetDirectory = System.IO.Path.GetDirectoryName(Path);
        string targetFilePath = GenerateUniqueFilePath(
            targetDirectory ?? throw new InvalidOperationException(),
            _newFileName);
        File.Move(Path, targetFilePath);
    }

    public void ExtendPath()
    {
        Path = System.IO.Path.GetFullPath(Path);
    }

    public class Builder
    {
        private string? _path;
        private string? _newFileName;

        public Builder WithPath(string path)
        {
            _path = path;

            return this;
        }

        public Builder WithNewFileName(string newFileName)
        {
            _newFileName = newFileName;

            return this;
        }

        public CommandBuildResult Build()
        {
            return new CommandBuildResult.Success(new FileRenameCommand(
                _path ?? throw new ArgumentNullException(nameof(_path)),
                _newFileName ?? throw new ArgumentNullException(nameof(_newFileName))));
        }
    }

    public bool Equals(FileRenameCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _newFileName == other._newFileName && Path == other.Path;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FileRenameCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_newFileName, Path);
    }

    private static string GenerateUniqueFilePath(string directory, string fileName)
    {
        string filePath = System.IO.Path.Combine(directory, fileName);
        string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
        string fileExtension = System.IO.Path.GetExtension(fileName);

        int counter = 1;
        while (File.Exists(filePath))
        {
            filePath = System.IO.Path.Combine(directory, $"{fileNameWithoutExtension}({counter}){fileExtension}");
            counter++;
        }

        return filePath;
    }
}
