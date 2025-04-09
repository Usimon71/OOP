using Itmo.ObjectOrientedProgramming.Lab4.CurrentProgramStates;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public class LocalFileSystem : IFileSystem
{
    public string ConnectedRoot { get; private set; }

    public LocalFileSystem()
    {
        ConnectedRoot = string.Empty;
    }

    public void Connect(string path)
    {
        ConnectedRoot = path;
        Directory.SetCurrentDirectory(path);
    }

    public void Disconnect()
    {
        ConnectedRoot = string.Empty;
    }
}
