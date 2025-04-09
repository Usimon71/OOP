using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public interface IFileSystemComponentVisitor
{
    void Visit(FileFileSystemComponent component);

    void Visit(DirectoryFileSystemComponent component);
}
