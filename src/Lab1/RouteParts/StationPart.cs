namespace Itmo.ObjectOrientedProgramming.Lab1.RouteParts;

public class StationPart : IRoutePart
{
    private readonly double _maxVelocity;
    private readonly Load _load;

    public StationPart(Load load, double maxVelocity)
    {
        _load = load;
        _maxVelocity = maxVelocity;
    }

    public PartResult RunTrain(Train train)
    {
        if (train.Velocity > _maxVelocity)
        {
            return new PartResult.Failure("Velocity is greater than maximum arrival velocity for a station.");
        }

        train.AddPathTime(GetTimeAddon(_load));

        return new PartResult.Success();
    }

    private static double GetTimeAddon(Load load)
    {
        return load switch
        {
            Load.Low => 5,
            Load.Medium => 10,
            Load.High => 15,
            _ => 5,
        };
    }
}
