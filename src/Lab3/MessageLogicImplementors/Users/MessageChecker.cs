namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;

public class MessageChecker
{
    private readonly HashSet<UserMessage> _messages = [];

    public IReadOnlySet<UserMessage> Messages => _messages;

    public void AddMessage(UserMessage message)
    {
        _messages.Add(message);
    }

    public CheckMessageResult CheckMessage(Message message)
    {
        if (!_messages.TryGetValue(new UserMessage(message), out UserMessage? messageResult))
        {
            return new CheckMessageResult.Failure("Users has not this message.");
        }

        if (messageResult.IsChecked)
        {
            return new CheckMessageResult.Failure("The message has already been checked.");
        }

        messageResult.CheckMessage();

        return new CheckMessageResult.Success();
    }
}
