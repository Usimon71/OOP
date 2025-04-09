namespace Itmo.ObjectOrientedProgramming.Lab3;

public class PriorityAddressee : IAddressee
{
    private readonly IAddressee _addressee;
    private readonly Importance _minImportance;

    private PriorityAddressee(Importance minImportance, IAddressee addressee)
    {
        _minImportance = minImportance;
        _addressee = addressee;
    }

    public static Builder Build => new();

    public MessageProcessResult ProcessMessage(Message message)
    {
        if (_minImportance > message.Importance)
        {
            return new MessageProcessResult.Failure("Message has low importance.");
        }

        _addressee.ProcessMessage(message);

        return new MessageProcessResult.Success();
    }

    public class Builder
    {
        private Importance? _minImportance;
        private IAddressee? _addressee;

        public Builder WithImportance(Importance importance)
        {
            _minImportance = importance;

            return this;
        }

        public Builder WithAddressee(IAddressee addressee)
        {
            _addressee = addressee;

            return this;
        }

        public PriorityAddressee Build()
        {
            return new PriorityAddressee(
                _minImportance ?? throw new ArgumentNullException(nameof(_minImportance)),
                _addressee ?? throw new ArgumentNullException(nameof(_addressee)));
        }
    }
}
