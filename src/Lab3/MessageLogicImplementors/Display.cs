namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public class Display : IAddressee
{
    private readonly IDisplayDriver _displayDriver;

    private Display(IDisplayDriver displayDriver)
    {
        _displayDriver = displayDriver;
    }

    public static Builder Build => new();

    public MessageProcessResult ProcessMessage(Message message)
    {
        _displayDriver.ClearOutput();
        _displayDriver.WriteText(
            $"{message.Header}\n" +
            $"{message.Body}\n" +
            $"display\n");

        return new MessageProcessResult.Success();
    }

    public class Builder
    {
        private IDisplayDriver? _displayDriver;

        public Builder WithDisplayDriver(IDisplayDriver displayDriver)
        {
            _displayDriver = displayDriver;

            return this;
        }

        public Display Build()
        {
            return new Display(
                _displayDriver ?? throw new ArgumentNullException(nameof(_displayDriver)));
        }
    }
}
