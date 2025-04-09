using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

public class ValidatedConnectCommand : ICommand
{
    public LocalConnectCommand Command { get; }

    public ValidatedConnectCommand(LocalConnectCommand command)
    {
        Command = command;
    }

    public CommandExecutionResult Execute(ConnectedFileSystem fileSystem)
    {
        if (fileSystem.FileSystem is not LocalFileSystem)
        {
            return new CommandExecutionResult.Failure("Unsupported file system");
        }

        if (!LocalFileSystemPathValidator.IsValidDirectory(Command.Path)
            || !LocalFileSystemPathValidator.IsFullyQualifiedPath(Command.Path))
        {
            return new CommandExecutionResult.Failure($"Invalid path: {Command.Path}");
        }

        Command.Execute();

        return new CommandExecutionResult.Success();
    }
}
