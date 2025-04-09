namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class DirectoryFileSystemComponent : IFileSystemComponent
{
    public DirectoryFileSystemComponent(
        string name,
        IReadOnlyCollection<IFileSystemComponent> components)
    {
        Name = name;
        Components = components;
    }

    public string Name { get; }

    public IReadOnlyCollection<IFileSystemComponent> Components { get; }

    public void Accept(IFileSystemComponentVisitor visitor)
    {
        visitor.Visit(this);
    }

    public bool Equals(IFileSystemComponent? other)
    {
        if (other is null) return false;

        return Name == other.Name;
    }
}
