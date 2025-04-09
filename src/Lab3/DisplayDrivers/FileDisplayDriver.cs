using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;

public class FileDisplayDriver : IDisplayDriver
{
    private readonly string _filename;
    private Color _color;

    public FileDisplayDriver(string filename, Color color)
    {
        _filename = filename;
        _color = color;
    }

    public void ClearOutput()
    {
        File.WriteAllText(_filename, string.Empty);
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public void WriteText(string text)
    {
        File.WriteAllText(
            _filename,
            Crayon.Output.Rgb(_color.R, _color.G, _color.B).Text(text));
    }
}
