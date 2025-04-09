namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees.Users;

public class UserMessage : IEquatable<UserMessage>
{
    public Message Message { get; }

    public bool IsChecked { get; private set; }

    public UserMessage(Message message)
    {
        Message = message;
        IsChecked = false;
    }

    public CheckMessageResult CheckMessage()
    {
        if (IsChecked)
        {
            return new CheckMessageResult.Failure("Message has already been checked");
        }

        IsChecked = true;

        return new CheckMessageResult.Success();
    }

    public bool Equals(UserMessage? other)
    {
        return other is not null && (ReferenceEquals(this, other) || Message.Id.Equals(other.Message.Id));
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((UserMessage)obj)));
    }

    public override int GetHashCode()
    {
        return Message.Id.GetHashCode();
    }
}
