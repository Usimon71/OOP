using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public interface ICommand
{
    CommandExecutionResult Execute(ConnectedFileSystem fileSystem);
}
