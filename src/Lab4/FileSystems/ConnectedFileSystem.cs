using Itmo.ObjectOrientedProgramming.Lab4.CurrentProgramStates;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public class ConnectedFileSystem
{
    public bool Connected { get; private set; }

    public IFileSystem FileSystem { get; private set; }

    public ConnectedFileSystem(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
        Connected = false;
    }

    public void Connect(string path)
    {
        Connected = true;
        FileSystem.Connect(path);
    }

    public void Disconnect()
    {
        Connected = false;
        FileSystem.Disconnect();
    }
}
