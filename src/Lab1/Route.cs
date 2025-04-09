using Itmo.ObjectOrientedProgramming.Lab1.RouteParts;

namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Route : IRoute
{
    private readonly IList<IRoutePart> _routeParts;

    public Route(IList<IRoutePart> routeParts, double maxVelocity)
    {
        _routeParts = routeParts;
        _routeParts.Add(new EndRoutePart(maxVelocity));
    }

    public PathResult Run(Train train)
    {
        foreach (IRoutePart part in _routeParts)
        {
            PartResult passRes = part.RunTrain(train);
            if (passRes is PartResult.Failure)
            {
                return new PathResult.Failure(((PartResult.Failure)passRes).Message);
            }
        }

        return new PathResult.Success(train.PathTime);
    }
}
