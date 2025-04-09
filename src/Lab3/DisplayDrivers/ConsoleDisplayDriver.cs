using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayDrivers;

public class ConsoleDisplayDriver : IDisplayDriver
{
    private Color _color;

    public ConsoleDisplayDriver(Color color)
    {
        _color = color;
    }

    public void ClearOutput()
    {
        Console.Clear();
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public void WriteText(string text)
    {
        Console.WriteLine(
            Crayon.Output.Rgb(_color.R, _color.G, _color.B).Text(text));
    }
}
