namespace Itmo.ObjectOrientedProgramming.Lab3;

public class Message
{
    private Message(
        string header,
        string body,
        Importance importance)
    {
        Id = Guid.NewGuid();
        Header = header;
        Body = body;
        Importance = importance;
    }

    public static Builder Build => new();

    public Guid Id { get; private set; }

    public string Header { get; private set; }

    public string Body { get; private set; }

    public Importance Importance { get; private set; }

    public class Builder
    {
        private string? _header;
        private string? _body;
        private Importance? _importance;

        public Builder WithHeader(string header)
        {
            _header = header;

            return this;
        }

        public Builder WithBody(string body)
        {
            _body = body;

            return this;
        }

        public Builder WithImportance(string importance)
        {
            _importance = new Importance(importance);

            return this;
        }

        public Message Build()
        {
            return new Message(
                _header ?? throw new ArgumentNullException(nameof(_header)),
                _body ?? throw new ArgumentNullException(nameof(_body)),
                _importance ?? throw new ArgumentNullException(nameof(_importance)));
        }
    }
}
