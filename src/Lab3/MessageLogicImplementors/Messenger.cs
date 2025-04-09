using Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;

namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public class Messenger : IAddressee
{
    private readonly IConsoleWriter _consoleWriter;

    private Messenger(IConsoleWriter consoleWriter)
    {
        _consoleWriter = consoleWriter;
    }

    public static Builder Build => new();

    public MessageProcessResult ProcessMessage(Message message)
    {
        _consoleWriter.WriteLine($"{message.Header}\n" +
                          $"{message.Body}\n" +
                          "messenger\n");

        return new MessageProcessResult.Success();
    }

    public class Builder
    {
        private IConsoleWriter? _consoleWriter;

        public Builder WithConsoleWriter(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;

            return this;
        }

        public Messenger Build()
        {
            return new Messenger(
                _consoleWriter ?? throw new ArgumentNullException(nameof(_consoleWriter)));
        }
    }
}
