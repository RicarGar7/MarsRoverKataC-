namespace Test.Implementation.Movement;

public class Forward : Movement
{
    private readonly Position position;
    private readonly Facing.Facing facing;
    private readonly Planet planet;
    private readonly int speed;

    public Forward(Position position, Facing.Facing facing, Planet planet, int speed)
    {
        this.position = position;
        this.facing = facing;
        this.planet = planet;
        this.speed = speed;
    }

    public Either<Alert, Position> Move()
    {
        return facing.Value() switch
        {
            FacingValue.N => ToNorth(),
            FacingValue.S => ToSouth(),
            FacingValue.E => ToEast(),
            FacingValue.W => ToWest(),
        };
    }

    private Either<Alert, Position> ToWest()
    {
        var targetPosition = new Position(position._latitude, position._longitude - speed);
        var edgePosition = new Position(position._latitude, planet._surface._longitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }

    private Either<Alert, Position> ToEast()
    {
        var targetPosition = new Position(position._latitude, position._longitude + speed);
        var edgePosition = new Position(position._latitude, 0);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }

    private Either<Alert, Position> ToSouth()
    {
        var targetPosition = new Position(position._latitude - speed, position._longitude);
        var edgePosition = new Position(planet._surface._latitude, position._latitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }

    private Either<Alert, Position> ToNorth()
    {
        var targetPosition = new Position(position._latitude + speed, position._longitude);
        var edgePosition =new Position(0, position._longitude);
        return new LinearMovement(edgePosition, planet, targetPosition).Apply();
    }
}