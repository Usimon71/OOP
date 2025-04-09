namespace Itmo.ObjectOrientedProgramming.Lab3.Addressees;

public class Group : IAddressee
{
    private readonly IList<IAddressee> _addresses;

    private Group(IList<IAddressee> addresses)
    {
        _addresses = addresses;
    }

    public static Builder Build => new();

    public class Builder
    {
        private readonly IList<IAddressee> _builderAddresses = [];

        public Builder AddAddressee(IAddressee addressee)
        {
            _builderAddresses.Add(addressee);

            return this;
        }

        public Group Build()
        {
            return new Group(
            _builderAddresses);
        }
    }

    public MessageProcessResult ProcessMessage(Message message)
    {
        foreach (IAddressee addressee in _addresses)
        {
            addressee.ProcessMessage(message);
        }

        return new MessageProcessResult.Success();
    }
}
