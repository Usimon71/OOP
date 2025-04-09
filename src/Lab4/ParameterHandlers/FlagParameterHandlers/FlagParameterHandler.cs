namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers.FlagParameterHandlers;

public class FlagParameterHandler
{
    protected FlagParameterHandler? Next { get; private set; }

    private readonly string _key;

    public FlagParameterHandler(string key)
    {
        _key = key;
    }

    public FlagParameterHandler AddNext(FlagParameterHandler handler)
    {
        if (Next is null)
        {
            Next = handler;
        }
        else
        {
            Next.AddNext(handler);
        }

        return this;
    }

    public void Handle(IEnumerator<string> request, IDictionary<string, string> parameterValues)
    {
        if (request.Current == _key)
        {
            if (request.MoveNext() is false)
            {
                return;
            }

            parameterValues[_key] = request.Current;

            return;
        }

        Next?.Handle(request, parameterValues);
    }
}
