using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

public class ValidateFileMoveCommand : ICommand
{
    public FileMoveCommand Command { get; }

    public ValidateFileMoveCommand(FileMoveCommand command)
    {
        Command = command;
    }

    public CommandExecutionResult Execute(ConnectedFileSystem fileSystem)
    {
        if (fileSystem.FileSystem is not LocalFileSystem)
        {
            return new CommandExecutionResult.Failure("Unsupported file system");
        }

        if (fileSystem.Connected is false)
        {
            return new CommandExecutionResult.Failure("File system is not connected");
        }

        if (!LocalFileSystemPathValidator.IsFullyQualifiedPath(Command.SourcePath))
        {
            Command.ExtendSourcePath();
        }

        if (LocalFileSystemPathValidator.IsValidFile(Command.SourcePath) is false ||
            LocalFileSystemPathValidator.IsSubPath(fileSystem.FileSystem.ConnectedRoot, Command.SourcePath) is false)
        {
            return new CommandExecutionResult.Failure("Invalid path");
        }

        if (string.IsNullOrEmpty(Command.TargetPath))
        {
            Command.SetTargetPathToCurrent();
        }

        if (!LocalFileSystemPathValidator.IsFullyQualifiedPath(Command.TargetPath))
        {
            Command.ExtendTargetPath();
        }

        if (LocalFileSystemPathValidator.IsValidDirectory(Command.TargetPath) is false ||
            LocalFileSystemPathValidator.IsSubPath(fileSystem.FileSystem.ConnectedRoot, Command.TargetPath) is false)
        {
            return new CommandExecutionResult.Failure("Invalid path");
        }

        Command.Execute();

        return new CommandExecutionResult.Success();
    }
}
