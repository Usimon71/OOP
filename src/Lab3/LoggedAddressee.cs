namespace Itmo.ObjectOrientedProgramming.Lab3;

public class LoggedAddressee : IAddressee
{
    private readonly IAddressee _addressee;
    private readonly ILogger _logger;

    private LoggedAddressee(IAddressee addressee, ILogger logger)
    {
        _addressee = addressee;
        _logger = logger;
    }

    public static Builder Build => new();

    public MessageProcessResult ProcessMessage(Message message)
    {
        if (_addressee.ProcessMessage(message) is MessageProcessResult.Failure failure)
        {
            _logger.Log($"Message with Id {message.Id} is not received by addressee.");

            return failure;
        }

        _logger.Log($"Message with Id {message.Id} is received by addressee.");

        return new MessageProcessResult.Success();
    }

    public class Builder
    {
        private IAddressee? _addressee;
        private ILogger? _logger;

        public Builder WithAddressee(IAddressee addressee)
        {
            _addressee = addressee;

            return this;
        }

        public Builder WithLogger(ILogger logger)
        {
            _logger = logger;

            return this;
        }

        public LoggedAddressee Build()
        {
            return new LoggedAddressee(
                _addressee ?? throw new ArgumentNullException(nameof(_addressee)),
                _logger ?? throw new ArgumentNullException(nameof(_logger)));
        }
    }
}
