namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public record CommandBuildResult
{
    public sealed record Success(IBasicCommand Command) : CommandBuildResult;

    public sealed record InvalidPath : CommandBuildResult;

    public sealed record UnsupportedMode : CommandBuildResult;

    public sealed record DisconnectedFileSystem : CommandBuildResult;
}
