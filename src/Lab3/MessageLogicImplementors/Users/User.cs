using Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;

namespace Itmo.ObjectOrientedProgramming.Lab3.MessageLogicImplementors.Users;

public class User : IAddressee
{
    public MessageChecker MessageChecker { get; } = new MessageChecker();

    public MessageProcessResult ProcessMessage(Message message)
    {
        MessageChecker.AddMessage(new UserMessage(message));

        return new MessageProcessResult.Success();
    }

    public CheckMessageResult CheckMessage(Message message)
    {
        return MessageChecker.CheckMessage(message);
    }
}
