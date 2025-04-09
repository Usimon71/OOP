namespace Itmo.ObjectOrientedProgramming.Lab3;

public class Topic
{
    private Topic(string name, IList<IAddressee> adressees)
    {
        Name = name;
        _addressees = adressees;
    }

    private readonly IList<IAddressee> _addressees;

    public static Builder Build => new();

    public string Name { get; private set; }

    public void DistributeMessage(Message message)
    {
        foreach (IAddressee addressee in _addressees)
        {
            addressee.ProcessMessage(message);
        }
    }

    public class Builder()
    {
        private readonly IList<IAddressee> _builderAddressees = [];
        private string? _name;

        public Builder WithName(string name)
        {
            _name = name;

            return this;
        }

        public Builder AddAddressee(IAddressee addressee)
        {
            _builderAddressees.Add(addressee);

            return this;
        }

        public Topic Build()
        {
            return new Topic(
                _name ?? throw new ArgumentNullException(nameof(_name)),
                _builderAddressees);
        }
    }
}
