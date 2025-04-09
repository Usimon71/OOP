namespace Application.Models.Operations;

public record Operation(Guid Id, long AccountNumber, OperationType OperationType, DateTime Timestamp)
{
    public static OperationBuilder Build => new();

    public class OperationBuilder
    {
        private long? _accountNumber;
        private OperationType _operationType;
        private DateTime _timestamp;

        public OperationBuilder WithAccountNumber(long? accountNumber)
        {
            _accountNumber = accountNumber;

            return this;
        }

        public OperationBuilder WithOperationType(OperationType operationType)
        {
            _operationType = operationType;

            return this;
        }

        public OperationBuilder WithTimeStamp(DateTime timestamp)
        {
            _timestamp = timestamp;

            return this;
        }

        public Operation Build()
        {
            return new Operation(
                Guid.NewGuid(),
                _accountNumber ?? throw new ArgumentNullException(nameof(_accountNumber)),
                _operationType,
                _timestamp);
        }
    }
}
