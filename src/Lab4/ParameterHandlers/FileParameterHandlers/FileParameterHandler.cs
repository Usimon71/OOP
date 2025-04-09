namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FileParameterHandlers;

public class FileParameterHandler : ParameterHandlerBase
{
    protected FileParameterHandler? NextFileParameterHandler { get; private set; }

    public FileParameterHandler AddNextFileParameterHandler(FileParameterHandler handler)
    {
        if (NextFileParameterHandler is null)
        {
            NextFileParameterHandler = handler;
        }
        else
        {
            NextFileParameterHandler.AddNextFileParameterHandler(handler);
        }

        return this;
    }

    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "file")
        {
            return Next?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("File command is not specified.");
        }

        return NextFileParameterHandler?.Handle(request);
    }
}
