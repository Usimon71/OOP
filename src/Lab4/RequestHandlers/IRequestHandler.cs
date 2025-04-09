using Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

namespace Itmo.ObjectOrientedProgramming.Lab4.RequestHandlers;

public interface IRequestHandler
{
    ParameterHandleResult? HandleRequest(string request);
}
