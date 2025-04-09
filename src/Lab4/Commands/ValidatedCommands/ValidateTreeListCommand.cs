using Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

public class ValidateTreeListCommand : ICommand
{
    public LocalTreeListCommand Command { get; }

    public ValidateTreeListCommand(LocalTreeListCommand command)
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
            return new CommandExecutionResult.Failure("File system is not connected");
        }

        Command.Execute();

        return new CommandExecutionResult.Success();
    }
}
