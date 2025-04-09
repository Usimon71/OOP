using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Writers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.LocalCommands;

public class TreeListVisitor : IFileSystemComponentVisitor, IEquatable<TreeListVisitor>
{
    private const string FileSymbol = "f-> ";
    private const string DirectorySymbol = "d-> ";
    private readonly IWriter _writer;
    private int _depth;

    public static Builder Build => new();

    private int MaxDepth { get; }

    public TreeListVisitor(IWriter writer, int? maxDepth)
    {
        _writer = writer;
        MaxDepth = maxDepth ?? 1;
    }

    public void Visit(FileFileSystemComponent component)
    {
        WriteIndented(component);
    }

    public void Visit(DirectoryFileSystemComponent component)
    {
        WriteIndented(component);

        if (_depth >= MaxDepth) return;

        _depth += 1;

        foreach (IFileSystemComponent innerComponent in component.Components)
        {
            innerComponent.Accept(this);
        }

        _depth -= 1;
    }

    private void WriteIndented(IFileSystemComponent component)
    {
        if (_depth is not 0)
        {
            _writer.Write(string.Concat(Enumerable.Repeat("   ", _depth)));
            _writer.Write(component is FileFileSystemComponent ? FileSymbol : DirectorySymbol);
        }

        _writer.WriteLine(component.Name);
    }

    public class Builder
    {
        private IWriter? _writer;
        private int _maxDepth;

        public Builder WithWriter(IWriter writer)
        {
            _writer = writer;

            return this;
        }

        public Builder WithMaxDepth(int maxDepth)
        {
            _maxDepth = maxDepth;

            return this;
        }

        public TreeListVisitor Build()
        {
            return new TreeListVisitor(
                _writer ?? throw new ArgumentNullException(),
                _maxDepth);
        }
    }

    public bool Equals(TreeListVisitor? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return MaxDepth == other.MaxDepth;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TreeListVisitor)obj);
    }

    public override int GetHashCode()
    {
        return MaxDepth;
    }
}
