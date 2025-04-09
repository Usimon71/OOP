using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

public class ValidateFileShowCommand : ICommand
{
    public FileShowCommand Command { get; }

    public ValidateFileShowCommand(FileShowCommand command)
    {
        Command = command;
    }

    public CommandExecutionResult Execute(ConnectedFileSystem fileSystem)
    {
        if (fileSystem.FileSystem is not LocalFileSystem)
        {
            return new CommandExecutionResult.Failure("Unsupported file system");
        }

        if (!fileSystem.Connected)
        {
            return new CommandExecutionResult.Failure("FileSystem is not connected");
        }

        if (!LocalFileSystemPathValidator.IsFullyQualifiedPath(Command.Path))
        {
            Command.ExtendPath();
        }

        if (!LocalFileSystemPathValidator.IsValidFile(Command.Path)
            || !LocalFileSystemPathValidator.IsSubPath(fileSystem.FileSystem.ConnectedRoot, Command.Path))
        {
            return new CommandExecutionResult.Failure("Invalid file path");
        }

        Command.Execute();

        return new CommandExecutionResult.Success();
    }
}
