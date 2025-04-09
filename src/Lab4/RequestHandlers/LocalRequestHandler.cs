using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.TreeParameterHandlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.RequestHandlers;

public class LocalRequestHandler : IRequestHandler
{
    private readonly IParameterHandler _handler;

    public LocalRequestHandler(ConnectedFileSystem fileSystem)
    {
        _handler = new BaseHandler()
            .AddNext(new ConnectParameterHandler(fileSystem))
            .AddNext(new DisconnectParameterHandler(fileSystem))
            .AddNext(new TreeParameterHandler()
                .AddNextTreeParameterHandler(new TreeListParameterHandler())
                .AddNextTreeParameterHandler(new TreeGotoParameterHandler()))
            .AddNext(new FileParameterHandler()
                .AddNextFileParameterHandler(new FileMoveParameterHandler())
                .AddNextFileParameterHandler(new FileShowParameterHandler())
                .AddNextFileParameterHandler(new FileRenameParameterHandler())
                .AddNextFileParameterHandler(new FileCopyParameterHandler())
                .AddNextFileParameterHandler(new FileDeleteParameterHandler()));
    }

    public ParameterHandleResult? HandleRequest(string request)
    {
        IEnumerator<string> enumerator = GetEnumerator(request.Trim());

        return _handler.Handle(enumerator);
    }

    private static IEnumerator<string> GetEnumerator(string request)
    {
        return request.Split(" ").ToList().GetEnumerator();
    }
}
