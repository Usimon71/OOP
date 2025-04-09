namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class FileMoveCommand : IBasicCommand, IEquatable<FileMoveCommand>
{
    public string SourcePath { get; private set; }

    public string TargetPath { get; private set; }

    private FileMoveCommand(string sourcePath, string targetPath)
    {
        SourcePath = sourcePath;
        TargetPath = targetPath;
    }

    public static Builder Build => new();

    public void Execute()
    {
        string fileName = Path.GetFileName(SourcePath);
        string targetFilePath = GenerateUniqueFilePath(TargetPath, fileName);
        File.Move(SourcePath, targetFilePath);
    }

    public class Builder
    {
        private string? _sourcePath;
        private string? _targetPath;

        public Builder WithSourcePath(string sourcePath)
        {
            _sourcePath = sourcePath;

            return this;
        }

        public Builder WithTargetPath(string targetPath)
        {
            _targetPath = targetPath;

            return this;
        }

        public CommandBuildResult Build()
        {
            return new CommandBuildResult.Success(new FileMoveCommand(
                _sourcePath ?? throw new ArgumentNullException(nameof(_sourcePath)),
                _targetPath ?? throw new ArgumentNullException(nameof(_targetPath))));
        }
    }

    public void ExtendSourcePath()
    {
        SourcePath = Path.GetFullPath(SourcePath);
    }

    public void ExtendTargetPath()
    {
        TargetPath = Path.GetFullPath(TargetPath);
    }

    public void SetTargetPathToCurrent()
    {
        TargetPath = Directory.GetCurrentDirectory();
    }

    public bool Equals(FileMoveCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return SourcePath == other.SourcePath && TargetPath == other.TargetPath;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FileMoveCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SourcePath, TargetPath);
    }

    private static string GenerateUniqueFilePath(string directory, string fileName)
    {
        string filePath = Path.Combine(directory, fileName);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        string fileExtension = Path.GetExtension(fileName);

        int counter = 1;
        while (File.Exists(filePath))
        {
            filePath = Path.Combine(directory, $"{fileNameWithoutExtension}({counter}){fileExtension}");
            counter++;
        }

        return filePath;
    }
}
