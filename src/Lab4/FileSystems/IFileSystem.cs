namespace Itmo.ObjectOrientedProgramming.Lab4.CurrentProgramStates;

public interface IFileSystem
{
    public string ConnectedRoot { get; }

    void Connect(string path);

    void Disconnect();
}
