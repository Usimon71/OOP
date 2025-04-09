namespace Itmo.ObjectOrientedProgramming.Lab3.Loggers;

public class FileLogger
{
    private readonly string _filename;

    public FileLogger(string filename)
    {
        _filename = filename;
    }

    public void Log(string message)
    {
        File.WriteAllText(_filename, message);
    }
}
