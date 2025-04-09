namespace Itmo.ObjectOrientedProgramming.Lab1;

public class Train
{
    private readonly double _maxForce;
    private readonly double _mass;
    private readonly double _precision;
    private double _acceleration;

    public double Velocity { get; private set; }

    public double Path { get; private set; }

    public double PathTime { get; private set; }

    public Train(double maxForce, double mass, double precision)
    {
        _maxForce = maxForce;

        if (mass < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mass), "Mass must be positive");
        }

        _mass = mass;

        if (precision <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(precision), "Precision must be positive");
        }

        _precision = precision;
        Path = 0;
        PathTime = 0;
        Velocity = 0;
        _acceleration = 0;
    }

    public PartResult ReceiveForce(double force)
    {
        if (force > _maxForce)
        {
            return new PartResult.Failure("The force is too large.");
        }

        _acceleration = force / _mass;

        return new PartResult.Success();
    }

    public PartResult IterVelocity()
    {
        Velocity += _acceleration * _precision;

        return Velocity <= 0 ? new PartResult.Failure("Velocity must be positive") : new PartResult.Success();
    }

    public void IterPath()
    {
        Path += Velocity * _precision;
        PathTime += _precision;
    }

    public void AddPathTime(double time)
    {
        PathTime += time;
    }
}
