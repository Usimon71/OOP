using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3;

public interface IDisplayDriver
{
    void ClearOutput();

    void SetColor(Color color);

    void WriteText(string text);
}
