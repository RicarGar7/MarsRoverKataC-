namespace Test.Implementation.Movement;

public class Backwards : Movement
{
    private readonly Position position;
    private readonly Facing.Facing facing;
    private readonly Map map;
    private readonly int speed;

    public Backwards(Position position, Facing.Facing facing, Map map, int speed)
    {
        this.position = position;
        this.facing = facing;
        this.map = map;
        this.speed = speed;
    }

    public Either<Alert, Position> Move()
    {
        return facing.Value() switch
        {
            FacingValue.N => MoveToNorth(position, map),
            FacingValue.S => MoveToSouth(position, map),
            FacingValue.E => MoveToEast(position, map),
            FacingValue.W => MoveToWest(position, map),
            _ => Either<Alert, Position>.FromRight(position)
        };
    }

    private Either<Alert, Position> MoveToWest(Position position, Map map)
    {
        var targetPosition = new Position(position._latitude, position._longitude + speed);
        var edgePosition = new Position(position._latitude, map._surface._longitude);
        return new LinearMovement(edgePosition, map, targetPosition).Apply();
    }

    private Either<Alert, Position> MoveToEast(Position position, Map map)
    {
        var targetPosition = new Position(position._latitude, position._longitude - speed);
        var edgePosition = new Position(position._latitude, map._surface._longitude);
        return new LinearMovement(edgePosition, map, targetPosition).Apply();
    }

    private Either<Alert, Position> MoveToSouth(Position position, Map map)
    {
        var targetPosition = new Position(position._latitude + speed, position._longitude);
        var edgePosition = new Position(0, position._longitude);
        return new LinearMovement(edgePosition, map, targetPosition).Apply();
    }

    private Either<Alert, Position> MoveToNorth(Position position, Map map)
    {
        var targetPosition = new Position(position._latitude - speed, position._longitude);
        var edgePosition = new Position(map._surface._latitude, position._longitude);
        return new LinearMovement(edgePosition, map, targetPosition).Apply();
    }
}