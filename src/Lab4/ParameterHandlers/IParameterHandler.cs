namespace Itmo.ObjectOrientedProgramming.Lab4.ParameterHandlers;

public interface IParameterHandler
{
    IParameterHandler AddNext(IParameterHandler handler);

    ParameterHandleResult? Handle(IEnumerator<string> request);
}
