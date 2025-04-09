using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class LocalTreeListCommand : IBasicCommand, IEquatable<LocalTreeListCommand>
{
    private readonly TreeListVisitor _visitor;

    public static Builder Build => new();

    private LocalTreeListCommand(
        TreeListVisitor visitor)
    {
        _visitor = visitor;
    }

    public void Execute()
    {
        var factory = new LocalFileSystemComponentFactory();
        IFileSystemComponent component = factory.Create(Directory.GetCurrentDirectory());
        component.Accept(_visitor);
    }

    public class Builder
    {
        private int _maxDepth;
        private string? _mode;

        public Builder WithMaxDepth(int maxDepth)
        {
            _maxDepth = maxDepth;

            return this;
        }

        public Builder WithOutputMode(string mode)
        {
            _mode = mode;

            return this;
        }

        public CommandBuildResult Build()
        {
            if (_mode == "console")
            {
                TreeListVisitor visitor = TreeListVisitor.Build
                    .WithMaxDepth(_maxDepth)
                    .WithWriter(new ConsoleWriter())
                    .Build();
                return new CommandBuildResult.Success(new LocalTreeListCommand(visitor));
            }

            return new CommandBuildResult.UnsupportedMode();
        }
    }

    public bool Equals(LocalTreeListCommand? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _visitor.Equals(other._visitor);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LocalTreeListCommand)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_visitor);
    }
}
