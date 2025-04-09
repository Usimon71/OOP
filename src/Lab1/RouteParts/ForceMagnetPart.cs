namespace Itmo.ObjectOrientedProgramming.Lab1.RouteParts;

public class ForceMagnetPart : IRoutePart
{
    private readonly double _force;
    private readonly double _endpoint;

    public ForceMagnetPart(double force, double endpoint)
    {
        _force = force;
        _endpoint = endpoint;
    }

    public PartResult RunTrain(Train train)
    {
        PartResult resultForce = train.ReceiveForce(_force);
        if (resultForce is PartResult.Failure)
        {
            return resultForce;
        }

        while (train.Path < _endpoint)
        {
            PartResult resultVelocity = train.IterVelocity();
            if (resultVelocity is PartResult.Failure)
            {
                return resultVelocity;
            }

            train.IterPath();
        }

        return new PartResult.Success();
    }
}
