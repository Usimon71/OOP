namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class FileFileSystemComponent : IFileSystemComponent
{
    public FileFileSystemComponent(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Accept(IFileSystemComponentVisitor visitor)
    {
        visitor.Visit(this);
    }

    public bool Equals(IFileSystemComponent? other)
    {
        if (other is null) return false;
        return other.Name == Name;
    }
}
