namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

public class BaseHandler : ParameterHandlerBase
{
    public override ParameterHandleResult? Handle(IEnumerator<string> request)
    {
        if (request.MoveNext() is false)
        {
            return new ParameterHandleResult.Failure("Request must have command.");
        }

        return Next?.Handle(request);
    }
}
