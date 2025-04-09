namespace Itmo.ObjectOrientedProgramming.Lab1.RouteParts;

public class EndRoutePart : IRoutePart
{
    private readonly double _maxVelocity;

    public EndRoutePart(double maxVelocity)
    {
        _maxVelocity = maxVelocity;
    }

    public PartResult RunTrain(Train train)
    {
        if (train.Velocity > _maxVelocity)
        {
            return new PartResult.Failure("Train has crashed into end part");
        }

        return new PartResult.Success();
    }
}
