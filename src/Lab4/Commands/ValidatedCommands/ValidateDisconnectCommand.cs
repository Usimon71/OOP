using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;

public class ValidateDisconnectCommand : ICommand
{
    public DisconnectCommand Command { get; }

    public ValidateDisconnectCommand(DisconnectCommand command)
    {
        Command = command;
    }

    public CommandExecutionResult Execute(ConnectedFileSystem fileSystem)
    {
        Command.Execute();

        return new CommandExecutionResult.Success();
    }
}
