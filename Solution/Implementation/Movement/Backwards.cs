namespace Test.Implementation.Movement;

public class Backwards : Movement
{
    private readonly Position position;
    private readonly Planet planet;
    private readonly int speed;

    public Backwards(Position position, Facing.Facing facing, Planet planet, int speed): base(facing)
    {
        this.position = position;
        this.planet = planet;
        this.speed = speed;
    }

    protected override Result<Alert, Position> ToWest()
    {
        var targetPosition = new Position(position._latitude, position._longitude + speed);
        var edgePosition = new Position(position._latitude, planet._surface._longitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }

    protected override Result<Alert, Position> ToEast()
    {
        var targetPosition = new Position(position._latitude, position._longitude - speed);
        var edgePosition = new Position(position._latitude, planet._surface._longitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }

    protected override Result<Alert, Position> ToSouth()
    {
        var targetPosition = new Position(position._latitude + speed, position._longitude);
        var edgePosition = new Position(0, position._longitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }

    protected override Result<Alert, Position> ToNorth()
    {
        var targetPosition = new Position(position._latitude - speed, position._longitude);
        var edgePosition = new Position(planet._surface._latitude, position._longitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }
}