using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

public class ValidateTreeGotoCommand : ICommand
{
    public LocalTreeGotoCommand Command { get; }

    public ValidateTreeGotoCommand(LocalTreeGotoCommand command)
    {
        Command = command;
    }

    public CommandExecutionResult Execute(ConnectedFileSystem fileSystem)
    {
        if (fileSystem.FileSystem is not LocalFileSystem)
        {
            return new CommandExecutionResult.Failure("Unsupported file system");
        }

        if (!LocalFileSystemPathValidator.IsValidDirectory(Command.NewPath))
        {
            return new CommandExecutionResult.Failure("Invalid directory path");
        }

        if (!LocalFileSystemPathValidator.IsSubPath(fileSystem.FileSystem.ConnectedRoot, Command.NewPath))
        {
            return new CommandExecutionResult.Failure("Invalid directory path");
        }

        if (!fileSystem.Connected)
        {
            return new CommandExecutionResult.Failure("Not connected filesystem.");
        }

        if (!Path.IsPathFullyQualified(Command.NewPath))
        {
            Command.ExtendPath();
        }

        Command.Execute();

        return new CommandExecutionResult.Success();
    }
}
