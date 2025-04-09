using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.ValidatedCommands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

public class DisconnectParameterHandler : ParameterHandlerBase
{
    private readonly ConnectedFileSystem _fileSystem;

    public DisconnectParameterHandler(ConnectedFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "disconnect")
        {
            return Next?.Handle(request);
        }

        var command = new DisconnectCommand(_fileSystem);

        return new ParameterHandleResult.Success(
            new ValidateDisconnectCommand(command));
    }
}
