using Itmo.ObjectOrientedProgramming.Lab1;
using Itmo.ObjectOrientedProgramming.Lab1.RouteParts;
using Xunit;

namespace Lab1.Tests;

public class RouteTests
{
    private const double StandardForce = 10;
    private const double StandardMass = 10;
    private const double StandardPrecision = 0.1;
    private const double MaxVelocityBig = 100;
    private const double MaxVelocityFail = 14;
    private const double MaxVelocitySuccess = 100;

    [Fact]
    public void EndRouteMaxVelocitySuccessTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 20),
            new OrdinaryMagnetPart(120),
        };

        var route = new Route(routeParts, MaxVelocityBig);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Success);
    }

    [Fact]
    public void EndRouteMaxVelocityFailureTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 100),
            new OrdinaryMagnetPart(120),
        };
        var route = new Route(routeParts, MaxVelocityFail);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Failure);
    }

    [Fact]
    public void StationMaxVelocityTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 100),
            new OrdinaryMagnetPart(120),
            new StationPart(Load.Low, MaxVelocitySuccess),
            new OrdinaryMagnetPart(140),
        };
        var route = new Route(routeParts, MaxVelocitySuccess);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Success);
    }

    [Fact]
    public void StationMaxVelocityFailureTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 100),
            new StationPart(Load.Low, MaxVelocityFail),
            new OrdinaryMagnetPart(140),
        };
        var route = new Route(routeParts, MaxVelocitySuccess);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Failure);
    }

    [Fact]
    public void EndRouteMaxVelocityWithStationFailureTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 100),
            new OrdinaryMagnetPart(140),
            new StationPart(Load.Low, MaxVelocitySuccess),
            new OrdinaryMagnetPart(160),
        };
        var route = new Route(routeParts, MaxVelocityFail);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Failure);
    }

    [Fact]
    public void SlowDownForcePartSuccessTest()
    {
        // Arrange
        const double maxVelocityNorm = 10;
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 100),
            new OrdinaryMagnetPart(120),
            new ForceMagnetPart(-StandardForce, 170),
            new StationPart(Load.High, maxVelocityNorm),
            new OrdinaryMagnetPart(200),
            new ForceMagnetPart(StandardForce, 250),
            new OrdinaryMagnetPart(270),
            new ForceMagnetPart(-StandardForce, 320),
        };
        var route = new Route(routeParts, maxVelocityNorm);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Success);
    }

    [Fact]
    public void ZeroVelocityFailureTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new OrdinaryMagnetPart(120),
        };
        var route = new Route(routeParts, MaxVelocityBig);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Failure);
    }

    [Fact]
    public void NegativeVelocityFailureTest()
    {
        // Arrange
        var train = new Train(StandardForce, StandardMass, StandardPrecision);
        var routeParts = new List<IRoutePart>
        {
            new ForceMagnetPart(StandardForce, 100),
            new ForceMagnetPart(-2 * StandardForce, 200),
        };

        var route = new Route(routeParts, MaxVelocityBig);

        // Act
        PathResult result = route.Run(train);

        // Assert
        Assert.True(result is PathResult.Failure);
    }
}
