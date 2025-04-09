namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public interface IFileSystemComponent : IEquatable<IFileSystemComponent>
{
    string Name { get; }

    void Accept(IFileSystemComponentVisitor visitor);
}
