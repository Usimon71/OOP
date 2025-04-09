namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.TreeParameterHandlers;

public class TreeParameterHandler : ParameterHandlerBase
{
    protected TreeParameterHandler? NextTreeParameterHandler { get; private set; }

    public TreeParameterHandler AddNextTreeParameterHandler(TreeParameterHandler handler)
    {
        if (NextTreeParameterHandler is null)
        {
            NextTreeParameterHandler = handler;
        }
        else
        {
            NextTreeParameterHandler.AddNextTreeParameterHandler(handler);
        }

        return this;
    }

    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.Current is not "tree")
        {
            return Next?.Handle(request);
        }

        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("Tree command is not specified.");
        }

        return NextTreeParameterHandler?.Handle(request);
    }
}
