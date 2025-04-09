namespace Itmo.ObjectOrientedProgramming.Lab1.RouteParts;

public class OrdinaryMagnetPart : IRoutePart
{
    private readonly double _endpoint;

    public OrdinaryMagnetPart(double endpoint)
    {
        _endpoint = endpoint;
    }

    public PartResult RunTrain(Train train)
    {
        PartResult resForce = train.ReceiveForce(0);
        if (resForce is PartResult.Failure)
        {
            return resForce;
        }

        PartResult resVelocity = train.IterVelocity();
        if (resVelocity is PartResult.Failure)
        {
            return resVelocity;
        }

        while (train.Path < _endpoint)
        {
            train.IterPath();
        }

        return new PartResult.Success();
    }
}
